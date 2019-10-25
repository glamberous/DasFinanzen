using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    public class SaveExpense_Button : MonoBehaviour {
        [SerializeField] private ExpenseModelForm_View View = null;

        public void OnMouseDown() {
            ExpenseModelForm_Controller.SaveExpense(View.Model);
        }
    }
}

