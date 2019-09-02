using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExpenseManager : MonoBehaviour, ManagerInterface {
    [HideInInspector] public List<Expense> Expenses = new List<Expense>();

    // Initialization variables
    private ILookup<int, ExpenseData> ExpenseDatasLookup = new List<ExpenseData>().ToLookup(expense => expense.ID);
    private List<ExpenseData> CurrentExpenseDatas { get => ExpenseDatasLookup[Managers.Catagory.CurrentID].ToList<ExpenseData>(); }

    [SerializeField] private Expense ExpenseOriginal = null;
    private TileUIData ExpenseTileData = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Expense manager starting...");

        ExpenseTileData = new TileUIData(ExpenseOriginal.gameObject);

        status = ManagerStatus.Started;
    }

    public decimal GetExpensesTotal(int ID) {
        decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseDatasLookup[ID].ToList<ExpenseData>())
            total += expense.Amount;
        return total;
    }

    #region ExpensesView
    public void ConstructExpenseView() {
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
    public void DeconstructAddExpenseView() {
        Debug.Log("DeconstructExpenseView");
    }

    public void ConstructAddExpenseView() {
        Debug.Log("ConstructExpenseView");
    }

    #endregion

    #region Save/Load
    public void LoadData(List<ExpenseData> expenseDatas) => ExpenseDatasLookup = expenseDatas.ToLookup(expense => expense.ID);
    public List<ExpenseData> GetData() => ExpenseDatasLookup.SelectMany( expenseData => expenseData ).ToList<ExpenseData>();

    #endregion
}
