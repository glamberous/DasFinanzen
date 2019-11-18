using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI {
    public class ExpenseElement : Button_Int {
        [SerializeField] private TextMeshProUGUI DateTextMesh = null;
        [SerializeField] private TextMeshProUGUI NameTextMesh = null;
        [SerializeField] private TextMeshProUGUI ExpenseTextMesh = null;

        public void UpdateView(ExpenseModel Model) {
            ExpenseTextMesh.text = "$" + Model.Amount.ToString();
            NameTextMesh.text = Model.NameText;
            SetDate(Model.Date);
            SetColor(Model.Catagory.ColorCode);
            SetAction(Controller.Instance.PushEditExpenseWindow, Model.ExpenseID);
        }

        private void SetColor(string colorCode) {
            Color newColor = ColorConverter.HexToColor(colorCode);
            NameTextMesh.color = newColor;
            DateTextMesh.color = newColor;
            ExpenseTextMesh.color = newColor;
        }

        private void SetDate(DateTime date) => DateTextMesh.text = date.ToString("MM/dd");
    }
}

