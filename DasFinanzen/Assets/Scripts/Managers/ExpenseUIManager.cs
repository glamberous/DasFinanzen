using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ExpenseUIManager : MonoBehaviour, ManagerInterface {
    [HideInInspector] public List<Expense> ExpenseUIs = new List<Expense>();

    // Initialization variables
    [SerializeField] private Expense ExpenseOriginal = null;
    [SerializeField] private TextMeshProUGUI ExpenseViewTitle = null;
    private TileUIData ExpenseUIData = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Expense manager starting...");

        ExpenseUIData = new TileUIData(ExpenseOriginal.gameObject);

        status = ManagerStatus.Started;
    }

    public void ConstructExpenseView() {
        ExpenseViewTitle.text = Managers.Data.CurrentCatagoryData.NameText;
        ResetExpenseTileData();
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
            newExpense = Instantiate(original: ExpenseOriginal, parent: ExpenseUIData.Parent.transform) as Expense;
        newExpense.transform.localPosition = new Vector3(ExpenseUIData.StartPos.x, ExpenseUIData.StartPos.y - (Constants.CatagoryOffset * ExpenseUIData.Count), ExpenseUIData.StartPos.z);
        newExpense.Construct(myExpenseData);
        ExpenseUIs.Add(newExpense);
        ExpenseUIData.Count++;
    }

    public void DeconstructExpenseView() {
        //SaveExpenseDatas();
        ClearExpenses();
    }

    private void ClearExpenses() {
        for (int count = 1; count < ExpenseUIs.Count; count++)
            Destroy(ExpenseUIs[count]);
        ExpenseUIs = new List<Expense>();
    }
}
