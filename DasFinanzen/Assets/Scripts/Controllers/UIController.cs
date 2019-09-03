using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject ExpenseView = null;
    [SerializeField] private GameObject AddExpenseView = null;
    [SerializeField] private GameObject GraphView = null;

    private void Awake() {
        Messenger.AddListener(AppEvent.EXPENSE_VIEW_TOGGLE, OnExpenseViewToggled);
        Messenger<bool>.AddListener(AppEvent.ADD_EXPENSE_TOGGLE, OnAddExpenseToggled);

        AddExpenseView.SetActive(false);
        ExpenseView.SetActive(false);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(AppEvent.EXPENSE_VIEW_TOGGLE, OnExpenseViewToggled);
        Messenger<bool>.RemoveListener(AppEvent.ADD_EXPENSE_TOGGLE, OnAddExpenseToggled);
    }

    private void OnExpenseViewToggled() {
        if (ExpenseView.activeInHierarchy) {
            ExpenseView.SetActive(false);
            Managers.Expense.DeconstructExpenseView();
        }
        else {
            Managers.Expense.ConstructExpenseView();
            ExpenseView.SetActive(true);
        }
    }

    private void OnAddExpenseToggled(bool triggerSave) {
        if (triggerSave)
            Debug.Log("Save Triggered!");
        if (AddExpenseView.activeInHierarchy) {
            AddExpenseView.SetActive(false);
            Managers.Expense.DeconstructAddExpenseView();
        } else {
            Managers.Expense.ConstructAddExpenseView();
            AddExpenseView.SetActive(true);
        }
    }
}
