using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class AddExpense_Button : MonoBehaviour {
        public void OnMouseDown() => ExpenseList_Controller.AddExpenseClicked();
    }
}

