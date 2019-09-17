using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class EditExpenseManagerBehaviour : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI ViewTitle = null;
    [SerializeField] private TMP_InputField NameInputField = null;
    [SerializeField] private TMP_InputField AmountInputField = null;
    [SerializeField] private TextMeshProUGUI AmountTextProxy = null;
    [SerializeField] private GameObject DeleteButton = null;

    public EditExpenseManager Manager { get; private set; }
    public void Awake() => Manager = new EditExpenseManager(ViewTitle, NameInputField, AmountInputField, AmountTextProxy, DeleteButton);
}

public class EditExpenseManager : ManagerInterface {
    private TextMeshProUGUI ViewTitle = null;
    private TMP_InputField NameInputField = null;
    private TMP_InputField AmountInputField = null;
    private TextMeshProUGUI AmountTextProxy = null;
    private GameObject DeleteButton = null;

    private ExpenseData ExpensePointer = null;
    private ExpenseData TempExpense = null;

    public EditExpenseManager(TextMeshProUGUI viewTitle, TMP_InputField nameInputField, TMP_InputField amountInputField, TextMeshProUGUI amountTextProxy, GameObject deleteButton) {
        ViewTitle = viewTitle;
        NameInputField = nameInputField;
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
    }

    private void ConstructNewView() {
        ViewTitle.text = "Add Expense";
        TempExpense = new ExpenseData();
        NameInputField.text = TempExpense.NameText;
    }

    private void ConstructEditView(ExpenseData expenseData) {
        ViewTitle.text = "Edit Expense";
        ExpensePointer = expenseData;
        TempExpense = (ExpenseData)expenseData.Clone();
        NameInputField.text = TempExpense.NameText;
        AmountInputField.text = ConvertAmountForDisplay(TempExpense.Amount);
        DeleteButton.SetActive(true);
    }

    // This formula may look strange but I need to do this because the user input is actually a string that 
    // looks like an Int which then later gets a decimal forced in between the second and third digit from the right.
    public string ConvertAmountForDisplay(decimal amount) => ((int)(amount * 100)).ToString();

    public void UpdateEditExpenseAmount() {
        try { TempExpense.Amount = Convert.ToDecimal(AmountTextProxy.text); } 
        catch { TempExpense.Amount = 0.00m; }
    }

    public void UpdateEditExpenseName(string input) => TempExpense.NameText = input;
    public void UpdateEditExpenseDate() {
        //TODO later once I have more DateTime functionality finished
    }

    public void SaveEditExpense() {
        if (ExpensePointer != null) 
            Managers.Data.EditExpense(ExpensePointer, TempExpense);
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

#if UNITY_EDITOR
    public void InitializeTempExpense() => TempExpense = new ExpenseData();
    public void SetAmountTextProxyText(string input) => AmountTextProxy.text = input;
    public decimal GetTempExpenseAmount() => TempExpense.Amount;
#endif
}
