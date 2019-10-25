using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class DeleteExpense_Button : MonoBehaviour {
        [SerializeField] private ExpenseModelForm_View View = null;

        public void OnMouseDown() {
            ExpenseModelForm_Controller.DeleteExpense(View.Model);
        }
    }
}

