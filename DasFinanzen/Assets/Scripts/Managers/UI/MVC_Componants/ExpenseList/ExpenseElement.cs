using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI {
    public class ExpenseElement : Button {
        [HideInInspector] public int ExpenseID { get; private set; } = -1;
        public void SetExpenseID(int id) => ExpenseID = id;

        // Inherited from Generic_Button
        public override void OnMouseDown() => Controller.TriggerCommand(CommandID, ExpenseID.ToString());

        [SerializeField] private TextMeshProUGUI DateTextMesh = null;
        [SerializeField] private TextMeshProUGUI NameTextMesh = null;
        [SerializeField] private TextMeshProUGUI ExpenseTextMesh = null;

        public void UpdateView(ExpenseModel Model) {
            ExpenseTextMesh.text = "$" + Model.Amount.ToString();
            NameTextMesh.text = Model.NameText;
            SetDate(Model.Date);
            SetColor(Model.Catagory.ColorCode);
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

