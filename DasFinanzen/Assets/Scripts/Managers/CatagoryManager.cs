using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }

    // Catagories for other classes to reference.
    [System.NonSerialized] public Dictionary<int, Catagory> Catagories = new Dictionary<int, Catagory>();

    private const float CatagoryOffset = 30.0f;

    // Initialization Variables
    [SerializeField] private CatagoryData[] CatagoryDatas = null;
    [SerializeField] private Catagory DailyOriginal = null;
    [SerializeField] private Catagory MonthlyOriginal = null;

    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories();

        status = ManagerStatus.Started;
    }

    #region Initialization
    private void InitializeCatagories() {
        CatagoryUIData DailyUIData = new CatagoryUIData(DailyOriginal);
        CatagoryUIData MonthlyUIData = new CatagoryUIData(MonthlyOriginal);
        foreach (CatagoryData data in CatagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(data.ID, InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(data.ID, InitializeCatagory(data, DailyUIData));
        UpdateTileSize(MonthlyUIData);
        UpdateTileSize(DailyUIData);
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, CatagoryUIData UIData) {
        Catagory newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original;
        else {
            newCatagory = Instantiate(original: UIData.Original, parent: UIData.Parent.transform) as Catagory;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Construct(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }
    
    private void UpdateTileSize(CatagoryUIData UIData) => UIData.Tile.sizeDelta =
        new Vector2(UIData.DefaultSizeDelta.x, UIData.DefaultSizeDelta.y + (CatagoryOffset * (UIData.Count - 1)));

    #endregion

    public void LoadData(List<ExpenseData> expenseDatas) {
        var sortedExpenseDatas = expenseDatas.GroupBy(expense => expense.ID);
        foreach (var expenseDataGroup in sortedExpenseDatas)
            if (Catagories.ContainsKey(expenseDataGroup.Key))
                Catagories[expenseDataGroup.Key].LoadExpenses(expenseDataGroup.ToList<ExpenseData>());
            else
                Debug.Log($"ERROR: Catagory ID {expenseDataGroup.Key} could not be found. Expense Data was lost.");
    }

    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatas = new List<ExpenseData>();
        foreach (KeyValuePair<int, Catagory> catagory in Catagories)
            expenseDatas.AddRange(catagory.Value.GetExpenseDatas());
        return expenseDatas;
    }
}

internal class CatagoryUIData {
    public Catagory Original;
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;
    public int Count = 0;

    public CatagoryUIData(Catagory original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }
}

