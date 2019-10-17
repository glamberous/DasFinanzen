using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]

public class OpenEditExpenseView : MonoBehaviour {
    private void OnMouseDown() {
        ExpenseData expenseData = gameObject.GetComponent<ExpenseElement>()?.Data;
        Messenger<ExpenseData>.Broadcast(AppEvent.OPEN_EDIT_EXPENSE_VIEW, expenseData);
    }
}
