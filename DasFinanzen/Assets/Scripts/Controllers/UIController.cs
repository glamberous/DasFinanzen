using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject ExpenseView = null;
    [SerializeField] private GameObject EditExpenseView = null;
    [SerializeField] private GameObject GraphView = null;

    private void Awake() {
        Messenger.AddListener(AppEvent.TOGGLE_EXPENSE_VIEW, OnExpenseViewToggled);
        Messenger<bool>.AddListener(AppEvent.CLOSE_EDIT_EXPENSE_VIEW, OnEditExpenseClose);
        Messenger<ExpenseData>.AddListener(AppEvent.OPEN_EDIT_EXPENSE_VIEW, OnEditExpenseOpen);

        EditExpenseView.SetActive(false);
        ExpenseView.SetActive(false);
    }

    private void OnExpenseViewToggled() {
        if (ExpenseView.activeInHierarchy) {
            ExpenseView.SetActive(false);
            Managers.ExpenseUI.DeconstructExpenseView();
        }
        else {
            Managers.ExpenseUI.ConstructExpenseView();
            ExpenseView.SetActive(true);
        }
    }

    private void OnEditExpenseClose(bool triggerSave) {
        if (triggerSave)
            Managers.EditExpenseUI.SaveEditExpense();
        EditExpenseView.SetActive(false);
        Managers.EditExpenseUI.DeconstructEditExpenseView();
    }

    private void OnEditExpenseOpen(ExpenseData expenseData) {
        Managers.EditExpenseUI.ConstructEditExpenseView(expenseData);
        EditExpenseView.SetActive(true);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(AppEvent.TOGGLE_EXPENSE_VIEW, OnExpenseViewToggled);
        Messenger<bool>.RemoveListener(AppEvent.CLOSE_EDIT_EXPENSE_VIEW, OnEditExpenseClose);
        Messenger<ExpenseData>.RemoveListener(AppEvent.OPEN_EDIT_EXPENSE_VIEW, OnEditExpenseOpen);
    }
}
