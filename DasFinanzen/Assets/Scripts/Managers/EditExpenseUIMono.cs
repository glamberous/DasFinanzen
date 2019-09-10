using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class EditExpenseUIMono : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI ViewTitle = null;
    [SerializeField] private TextMeshProUGUI NamePlaceholder = null;
    [SerializeField] private TMP_InputField AmountInputField = null;
    [SerializeField] private TextMeshProUGUI AmountTextProxy = null;
    [SerializeField] private GameObject DeleteButton = null;

    public EditExpenseUIManager Manager { get; private set; }
    public void Awake() {
        Manager = new EditExpenseUIManager();
        Manager.LoadMonoVariables(ViewTitle, NamePlaceholder, AmountInputField, AmountTextProxy, DeleteButton);
    }
}

public class EditExpenseUIManager : ManagerInterface {
    private TextMeshProUGUI ViewTitle = null;
    private TextMeshProUGUI NamePlaceholder = null;
    private TMP_InputField AmountInputField = null;
    private TextMeshProUGUI AmountTextProxy = null;
    private GameObject DeleteButton = null;

    public void LoadMonoVariables(TextMeshProUGUI viewTitle, TextMeshProUGUI namePlaceholder, TMP_InputField amountInputField, TextMeshProUGUI amountTextProxy, GameObject deleteButton) {
        ViewTitle = viewTitle;
        NamePlaceholder = namePlaceholder;
        AmountInputField = amountInputField;
        AmountTextProxy = amountTextProxy;
        DeleteButton = deleteButton;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Edit Expense Manager starting...");
        DeleteButton.SetActive(false);

        status = ManagerStatus.Started;
    }

    public void ConstructEditExpenseView(ExpenseData expenseData) {
        if (expenseData == null)
            ConstructNewView();
        else
            ConstructEditView(expenseData);
        NamePlaceholder.text = TempExpense.NameText;
    }

    private ExpenseData ExpensePointer = null;
    private ExpenseData TempExpense = null;

    private void ConstructNewView() {
        ViewTitle.text = "Add Expense";
        TempExpense = new ExpenseData();
    }

    private void ConstructEditView(ExpenseData expenseData) {
        ViewTitle.text = "Edit Expense";
        ExpensePointer = expenseData;
        TempExpense = (ExpenseData)expenseData.Clone();
        AmountInputField.text = ((int)(expenseData.Amount * 100)).ToString();
        DeleteButton.SetActive(true);
    }

    public void UpdateEditExpenseAmount() {
        Debug.Log($"Decimal being saved: {AmountTextProxy.text}");
        TempExpense.Amount = Convert.ToDecimal(AmountTextProxy.text);
    }
    public void UpdateEditExpenseName(string input) => TempExpense.NameText = input;
    public void UpdateEditExpenseDate() {
        //TODO later once I have more DateTime functionality finished
    }

    public void SaveEditExpense() {
        if (ExpensePointer != null) {
            ExpensePointer.CopyData(TempExpense);
            Debug.Log("Data Updated.");
            Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
        }
        else
            Managers.Data.AddExpense(TempExpense);
    }

    public void DeleteEditExpense() => Managers.Data.RemoveExpense(ExpensePointer);

    public void DeconstructEditExpenseView() {
        AmountInputField.text = "";
        ExpensePointer = null;
        TempExpense = null;
        DeleteButton.SetActive(false);
    }
}
