using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ExpenseManager : MonoBehaviour, ManagerInterface {
    [HideInInspector] public List<Expense> Expenses = new List<Expense>();

    // Working Expense Data Variables
    private Dictionary<int, List<ExpenseData>> ExpenseDatasDict = new Dictionary<int, List<ExpenseData>>();
    private List<ExpenseData> CurrentExpenseDatas { get => ExpenseDatasDict[Managers.Catagory.CurrentID]; }

    // Initialization variables
    [SerializeField] private Expense ExpenseOriginal = null;
    [SerializeField] private TextMeshProUGUI ExpenseViewTitle = null;
    private TileUIData ExpenseTileData = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Expense manager starting...");

        ExpenseTileData = new TileUIData(ExpenseOriginal.gameObject);

        status = ManagerStatus.Started;
    }

    public decimal GetExpensesTotal(int ID) {
        decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseDatasDict[ID])
            total += expense.Amount;
        return total;
    }

    #region ExpensesView
    public void ConstructExpenseView() {
        ExpenseViewTitle.text = Managers.Catagory.CurrentCatagory.NameText;
        ResetExpenseTileData();
        if (CurrentExpenseDatas.Count != 0)
            InitializeRegularExpenseView();
        else
            InitializeEmptyExpenseView();
    }

    private void ResetExpenseTileData() {
        ExpenseTileData.Count = 0;
        ExpenseTileData.Parent.SetActive(true);
    }

    private void InitializeRegularExpenseView() {
        foreach (ExpenseData data in CurrentExpenseDatas)
            InitializeExpense(data);
        ExpenseTileData.UpdateTileSize();
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
        Expenses.Add(newExpense);
        ExpenseTileData.Count++;
    }

    public void DeconstructExpenseView() {
        SaveExpenseDatas();
        ClearExpenses();
    }

    private void SaveExpenseDatas() {
        //TODO Finish this method
    }

    private void ClearExpenses() {
        for (int count = 1; count < Expenses.Count; count++)
            Destroy(Expenses[count]);
        Expenses = new List<Expense>();
    }
    #endregion

    #region AddExpenseView

    private ExpenseData ExpenseToAdd = null;

    public void DeconstructAddExpenseView() {
        Debug.Log("DeconstructExpenseView");
    }

    public void ConstructAddExpenseView() {
        Debug.Log("ConstructExpenseView");
    }

    #endregion

    #region Save/Load

    public void LoadData(List<ExpenseData> expenseDatas) {
        foreach (ExpenseData expenseData in expenseDatas) {
            if (!ExpenseDatasDict.ContainsKey(expenseData.ID))
                ExpenseDatasDict[expenseData.ID] = new List<ExpenseData>();
            ExpenseDatasDict[expenseData.ID].Add(expenseData);
        }
    }
    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatasToExport = new List<ExpenseData>();
        foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in ExpenseDatasDict)
            expenseDatasToExport.AddRange(expenseDatas.Value);
        return expenseDatasToExport;
    }

    #endregion
}
