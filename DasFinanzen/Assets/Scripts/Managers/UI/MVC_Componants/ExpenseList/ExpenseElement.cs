using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExpenseElement : MonoBehaviour, IControllerElement {
        public int ExpenseID { get; private set; } = -1;
        public void SetExpenseID(int id) => ExpenseID = id;

        private IController Controller = null;
        public void SetController(IController controller) => Controller = controller;

        private int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        private TextMeshProUGUI DateTextMesh = null;
        private TextMeshProUGUI NameTextMesh = null;
        private TextMeshProUGUI ExpenseTextMesh = null;

        public void Awake() {
            DateTextMesh = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            ExpenseTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        }

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

        public void OnMouseDown() =>  Controller.TriggerCommand(CommandID, ExpenseID.ToString());
    }
}

