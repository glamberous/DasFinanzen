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
        public override void OnMouseUp() {
            if (GetMouseAsWorldPoint() == OriginalMouseCoord)
                Controller.TriggerCommand(CommandID, ExpenseID.ToString());
        }

        private RectTransform TileRect = null;
        public void SetTileRect(RectTransform tile) => TileRect = tile;

        private Vector3 OriginalMouseCoord;
        private Vector3 mOffset;
        private float mZCoord;
        private float mXCoord;

        public void OnMouseDown() {
            mZCoord = Camera.main.WorldToScreenPoint(TileRect.transform.position).z;
            mXCoord = Camera.main.WorldToScreenPoint(TileRect.transform.position).x;
            OriginalMouseCoord = GetMouseAsWorldPoint();
            mOffset = TileRect.transform.position - OriginalMouseCoord;
        }

        private Vector3 GetMouseAsWorldPoint() {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = mZCoord;
            mousePoint.x = mXCoord;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        public void OnMouseDrag() {
            TileRect.transform.position = GetMouseAsWorldPoint() + mOffset;
        }

        //Maybe Refactor everything above this.

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

