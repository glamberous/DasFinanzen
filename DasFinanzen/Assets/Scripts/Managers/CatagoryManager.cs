using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }

    // Catagories for other classes to reference.
    [System.NonSerialized] public Dictionary<int, Catagory> Catagories = new Dictionary<int, Catagory>();
    [System.NonSerialized] public Catagory SelectedCatagory = null;
    [System.NonSerialized] public List<Expense> SelectedCatagoryExpenses = new List<Expense>();

    // Initialization Variables
    [SerializeField] private CatagoryData[] CatagoryDatas = null;
    [SerializeField] private Catagory DailyOriginal = null;
    [SerializeField] private Catagory MonthlyOriginal = null;
    [SerializeField] private Expense ExpenseOriginal = null;

    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories();

        status = ManagerStatus.Started;
    }

    #region Catagories
    private void InitializeCatagories() {
        CatagoryUIData DailyUIData = new CatagoryUIData(DailyOriginal);
        CatagoryUIData MonthlyUIData = new CatagoryUIData(MonthlyOriginal);
        foreach (CatagoryData data in CatagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(data.ID, InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(data.ID, InitializeCatagory(data, DailyUIData));
        MonthlyUIData.UpdateTileSize();
        DailyUIData.UpdateTileSize();
        // Null Variables so Garbage Collector will pick them up.
        MonthlyUIData = null;
        DailyUIData = null;
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, CatagoryUIData UIData) {
        Catagory newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original;
        else {
            newCatagory = Instantiate(original: UIData.Original, parent: UIData.Parent.transform) as Catagory;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Construct(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }

    #endregion

    #region Save/Load
    public void LoadData(List<ExpenseData> expenseDatas) {
        var sortedExpenseDatas = expenseDatas.GroupBy(expense => expense.ID);
        foreach (var expenseDataGroup in sortedExpenseDatas)
            if (Catagories.ContainsKey(expenseDataGroup.Key))
                Catagories[expenseDataGroup.Key].LoadExpenses(expenseDataGroup.ToList<ExpenseData>());
            else
                Debug.Log($"ERROR: Catagory ID {expenseDataGroup.Key} could not be found. Expense Data was lost.");
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
    }

    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatas = new List<ExpenseData>();
        foreach (KeyValuePair<int, Catagory> catagory in Catagories)
            expenseDatas.AddRange(catagory.Value.GetExpenseDatas());
        return expenseDatas;
    }
    #endregion

    #region Expenses


    public void PrepareSubCatagoryView(int ID) {
        ClearExpenses();
        SetSelectedCatagory(ID);
        InitializeExpenses();
    }

    private void ClearExpenses() {
        int count = 0;
        foreach (Expense expense in SelectedCatagoryExpenses) {
            if (count++ == 0)
                continue;
            else
                Destroy(expense);
        }
        SelectedCatagoryExpenses = new List<Expense>();
    }

    private void SetSelectedCatagory(int ID) => SelectedCatagory = Catagories[ID];

    private void InitializeExpenses() {
        ExpenseUIData expenseUIData = new ExpenseUIData(ExpenseOriginal);
        foreach (ExpenseData data in SelectedCatagory.GetExpenseDatas())
            SelectedCatagoryExpenses.Add(InitializeExpense(data, expenseUIData));
        expenseUIData.UpdateTileSize();
        expenseUIData = null; //Null variable so Garbage Collector will pick it up.
    }

    private Expense InitializeExpense(ExpenseData myExpenseData, ExpenseUIData UIData) {
        Expense newExpense;
        if (UIData.Count == 0)
            newExpense = UIData.Original;
        else {
            newExpense = Instantiate(original: UIData.Original, parent: UIData.Parent.transform) as Expense;
            newExpense.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newExpense.Construct(myExpenseData);
        UIData.Count++;
        return newExpense;
    }

    public void ExitSubCatagoryView() {
        SaveExpenseDatas();
    }

    private void SaveExpenseDatas() {
        
    }
    #endregion
}

abstract class UIData {
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;
    public int Count = 0;

    public void Construct(GameObject parent) {
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }

    public void UpdateTileSize() => Tile.sizeDelta =
        new Vector2(DefaultSizeDelta.x, DefaultSizeDelta.y + (Constants.CatagoryOffset * (Count - 1)));
}

internal class CatagoryUIData : UIData {
    public Catagory Original;
    
    public CatagoryUIData(Catagory original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        base.Construct(Parent);
    }
}

internal class ExpenseUIData : UIData {
    public Expense Original;

    public ExpenseUIData(Expense original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        base.Construct(Parent);
    }
}

internal static class Constants {
    public const float CatagoryOffset = 30.0f;
}
