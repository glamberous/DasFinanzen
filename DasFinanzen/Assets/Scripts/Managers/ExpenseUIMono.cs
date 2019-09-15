using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ExpenseUIMono : MonoBehaviour {
    // Initialization variables
    [SerializeField] private Expense ExpenseOriginal = null;
    [SerializeField] private TextMeshProUGUI ExpenseViewTitle = null;

    public ExpenseUIManager Manager { get; private set; }
    public void Awake() => Manager = new ExpenseUIManager(ExpenseOriginal, ExpenseViewTitle);
}

public class ExpenseUIManager : ManagerInterface {
    public List<Expense> ExpenseUIs = new List<Expense>();

    // Initialization variables
    private Expense ExpenseOriginal = null;
    private TextMeshProUGUI ExpenseViewTitle = null;
    private TileUIData ExpenseUIData = null;

    public ExpenseUIManager(Expense expenseOriginal, TextMeshProUGUI expenseViewTitle) {
        ExpenseOriginal = expenseOriginal;
        ExpenseViewTitle = expenseViewTitle;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Expense manager starting...");

        ExpenseUIData = new TileUIData(ExpenseOriginal.gameObject);
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, OnExpensesUpdated);

        status = ManagerStatus.Started;
    }

    private void OnExpensesUpdated() {
        DeconstructExpenseView();
        ConstructExpenseView();
    }

    public void ConstructExpenseView() {
        Debug.Log("Construct Expense View.");
        ExpenseViewTitle.text = Managers.Data.CurrentCatagoryData.NameText;
        if (Managers.Data.CurrentExpenseDatas.Count != 0)
            InitializeRegularExpenseView();
        else
            InitializeEmptyExpenseView();
    }

    private void ResetExpenseTileData() {
        ExpenseUIData.Count = 0;
        ExpenseUIData.Parent.SetActive(true);
    }

    private void InitializeRegularExpenseView() {
        foreach (ExpenseData data in Managers.Data.CurrentExpenseDatas)
            InitializeExpense(data);
        ExpenseUIData.UpdateTileSize();
    }

    private void InitializeEmptyExpenseView() {
        ExpenseUIData.Parent.SetActive(false);
        // Probably needs more Code here for unique UI when Empty.
    }

    private void InitializeExpense(ExpenseData myExpenseData) {
        Expense newExpense;
        if (ExpenseUIData.Count == 0)
            newExpense = ExpenseOriginal;
        else
            newExpense = GameObject.Instantiate(original: ExpenseOriginal, parent: ExpenseUIData.Parent.transform) as Expense;
        newExpense.transform.localPosition = new Vector3(ExpenseUIData.StartPos.x, ExpenseUIData.StartPos.y - (Constants.CatagoryOffset * ExpenseUIData.Count), ExpenseUIData.StartPos.z);
        newExpense.Construct(myExpenseData);
        ExpenseUIs.Add(newExpense);
        ExpenseUIData.Count++;
    }

    public void DeconstructExpenseView() {
        Debug.Log("Deconstruct Expense View.");
        ResetExpenseTileData();
        for (int Count = 1; Count < ExpenseUIs.Count; Count++) 
            GameObject.Destroy(ExpenseUIs[Count].gameObject);
        ExpenseUIs = new List<Expense>();
    }
}
