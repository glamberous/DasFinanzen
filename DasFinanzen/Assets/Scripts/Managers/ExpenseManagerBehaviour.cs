using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

/* TODO
public class ExpenseManagerBehaviour : MonoBehaviour {
    // Initialization variables
    [SerializeField] private Expense ExpenseOriginal = null;
    [SerializeField] private TextMeshProUGUI ExpenseViewTitle = null;

    public ExpenseManager Manager { get; private set; }
    public void Awake() => Manager = new ExpenseManager(ExpenseOriginal, ExpenseViewTitle);
}

public class ExpenseManager : ManagerInterface {
    public List<Expense> ExpenseUIs = new List<Expense>();

    // Initialization variables
    private Expense ExpenseOriginal = null;
    private TextMeshProUGUI ExpenseViewTitle = null;
    private TileUIData ExpenseUIData = null;

    public ExpenseManager(Expense expenseOriginal, TextMeshProUGUI expenseViewTitle) {
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

    
}
*/