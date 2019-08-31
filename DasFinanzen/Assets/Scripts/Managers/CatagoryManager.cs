using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    // Variables for other classes to reference.
    [System.NonSerialized] public Dictionary<int, Catagory> Catagories = new Dictionary<int, Catagory>();
    [System.NonSerialized] public Catagory SelectedCatagory = null;
    [System.NonSerialized] public List<Expense> SelectedCatagoryExpenses = new List<Expense>();

    // Initialization Variables
    [SerializeField] private CatagoryData[] CatagoryDatas = null;
    [SerializeField] private Catagory DailyOriginal = null;
    [SerializeField] private Catagory MonthlyOriginal = null;
    [SerializeField] private Expense ExpenseOriginal = null;
    private TileUIData ExpenseTileData = null;

    //private List<BoxCollider2D> MainColliders = new List<BoxCollider2D>();
    //private List<BoxCollider2D> SubCatagoryColliders = new List<BoxCollider2D>();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories();

        status = ManagerStatus.Started;
    }

    #region MainView
    private void InitializeCatagories() {
        TileUIData DailyUIData = new TileUIData(DailyOriginal.gameObject);
        TileUIData MonthlyUIData = new TileUIData(MonthlyOriginal.gameObject);
        foreach (CatagoryData data in CatagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(data.ID, InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(data.ID, InitializeCatagory(data, DailyUIData));
        MonthlyUIData.UpdateTileSize();
        DailyUIData.UpdateTileSize();
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, TileUIData UIData) {
        Catagory newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original.GetComponent<Catagory>();
        else {
            newCatagory = Instantiate(original: UIData.Original.GetComponent<Catagory>(), parent: UIData.Parent.transform) as Catagory;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Construct(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }
    #endregion

    #region ExpensesView
    public void ConstructSubCatagoryView(int ID) {
        SetSelectedCatagory(ID);
        InitializeExpenses();
    }

    private void SetSelectedCatagory(int ID) => SelectedCatagory = Catagories[ID];

    private void InitializeExpenses() {
        ResetExpenseTileData();
        if (SelectedCatagory.GetExpenseDatas().Count != 0)
            InitializeRegularExpenseView();
        else
            InitializeEmptyExpenseView();
    }

    private void InitializeRegularExpenseView() {
        foreach (ExpenseData data in SelectedCatagory.GetExpenseDatas())
            InitializeExpense(data);
        ExpenseTileData.UpdateTileSize();
    }

    private void ResetExpenseTileData() {
        if (ExpenseTileData == null)
            ExpenseTileData = new TileUIData(ExpenseOriginal.gameObject);
        ExpenseTileData.Count = 0;
        ExpenseTileData.Parent.SetActive(true);
    }

    private void InitializeEmptyExpenseView() {
        ExpenseTileData.Parent.SetActive(false);
        // Probably needs more Code here for unique UI when Empty.
    }

    private void InitializeExpense(ExpenseData myExpenseData) {
        Expense newExpense;
        if (ExpenseTileData.Count == 0)
            newExpense = ExpenseOriginal;
        else 
            newExpense = Instantiate(original: ExpenseOriginal, parent: ExpenseTileData.Parent.transform) as Expense;
        newExpense.transform.localPosition = new Vector3(ExpenseTileData.StartPos.x, ExpenseTileData.StartPos.y - (Constants.CatagoryOffset * ExpenseTileData.Count), ExpenseTileData.StartPos.z);
        newExpense.Construct(myExpenseData);
        SelectedCatagoryExpenses.Add(newExpense);
        ExpenseTileData.Count++;
    }

    public void DeconstructSubCatagoryView() {
        SaveExpenseDatas();
        ClearExpenses();
    }

    private void SaveExpenseDatas() {
        //TODO Finish this method
    }

    private void ClearExpenses() {
        int count = 0;
        foreach (Expense expense in SelectedCatagoryExpenses) {
            if (count++ == 0)
                continue; // Spare ExpenseOriginal from being Destroyed
            else
                Destroy(expense);
        }
        SelectedCatagoryExpenses = new List<Expense>();
    }
    #endregion

    #region AddExpenseView
    public void DeconstructAddExpenseView() {
        Debug.Log("DeconstructExpenseView");
    }

    public void ConstructAddExpenseView() {
        Debug.Log("ConstructExpenseView");
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

}

internal class TileUIData {
    public GameObject Original;
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;
    public int Count = 0;

    public TileUIData(GameObject original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }

    public void UpdateTileSize() => Tile.sizeDelta =
        new Vector2(DefaultSizeDelta.x, DefaultSizeDelta.y + (Constants.CatagoryOffset * (Count - 1)));
}

internal static class Constants {
    public const float CatagoryOffset = 30.0f;
}
