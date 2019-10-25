using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExpenseElement : MonoBehaviour {
        public int ExpenseID;

        public void SetID(int id) => ExpenseID = id;

        private TextMeshProUGUI DateTextMesh = null;
        private TextMeshProUGUI NameTextMesh = null;
        private TextMeshProUGUI ExpenseTextMesh = null;
        private TextMeshProUGUI CurrencySymbol = null;

        public void Awake() {
            DateTextMesh = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            ExpenseTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            CurrencySymbol = ExpenseTextMesh.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void UpdateView(ExpenseModel Model) {
            ExpenseTextMesh.text = Model.Amount.ToString();
            NameTextMesh.text = Model.NameText;
            SetDate(Model.Date);
            SetColor(Model.Catagory.ColorCode);
        }

        private void SetColor(string colorCode) {
            Color newColor = ColorConverter.HexToColor(colorCode);
            NameTextMesh.color = newColor;
            DateTextMesh.color = newColor;
            ExpenseTextMesh.color = newColor;
            CurrencySymbol.color = newColor;
        }

        private void SetDate(DateTime date) => DateTextMesh.text = string.Format("{0}/{1}", date.Month.ToString(), date.Day.ToString());
    }
}

