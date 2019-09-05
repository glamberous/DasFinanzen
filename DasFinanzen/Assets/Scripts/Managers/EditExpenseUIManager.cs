using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class EditExpenseUIManager : MonoBehaviour, ManagerInterface {

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Edit Expense Manager starting...");
        //DeleteButton.SetActive(false);

        status = ManagerStatus.Started;
    }
    
    [SerializeField] private TextMeshProUGUI ViewTitle = null;
    [SerializeField] private TextMeshProUGUI NamePlaceholder = null;
    [SerializeField] private TMP_InputField AmountInputField = null;
    [SerializeField] private TextMeshProUGUI AmountTextProxy = null;
    [SerializeField] private GameObject DeleteButton = null;

    private ExpenseData ExpensePointer = null;
    private ExpenseData TempExpense = null;

    public void ConstructEditExpenseView(ExpenseData expenseData) {
        if (expenseData == null)
            ConstructNewView();
        else
            ConstructEditView(expenseData);
        NamePlaceholder.text = TempExpense.NameText;
        Debug.Log("ConstructExpenseView");
    }

    private void ConstructNewView() {
        ViewTitle.text = "Add Expense";
        TempExpense = new ExpenseData();
    }

    private void ConstructEditView(ExpenseData expenseData) {
        ViewTitle.text = "Edit Expense";
        ExpensePointer = expenseData;
        TempExpense = (ExpenseData)expenseData.Clone();
        AmountInputField.text = ((int)(expenseData.Amount * 100)).ToString();
        //DeleteButton.SetActive(true);
    }

    public void UpdateEditExpenseAmount() => TempExpense.Amount = Convert.ToDecimal(AmountTextProxy.text);
    public void UpdateEditExpenseName(string input) => TempExpense.NameText = input;
    public void UpdateEditExpenseDate() {
        //TODO later once I have more DateTime functionality finished
    }

    public void SaveEditExpense() {
        if (ExpensePointer != null)
            ExpensePointer.CopyData(TempExpense);
        else
            Managers.Data.AddExpense(TempExpense);
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
        Debug.Log("EditExpense Save Triggered.");
    }

    public void DeleteEditExpense() => Managers.Data.RemoveExpense(ExpensePointer);

    public void DeconstructEditExpenseView() {
        AmountInputField.text = "";
        ExpensePointer = null;
        TempExpense = null;
        //DeleteButton.SetActive(false);
        Debug.Log("DeconstructExpenseView");
    }


}
