using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBar_V : MonoBehaviour, IView {
        [SerializeField] private ColorBarElement OriginalColorBar = null;
        [SerializeField] private RectTransform CanvasRect = null;

        private List<ColorBarElement> ColorBarElements = new List<ColorBarElement>();

        public void Activate() {

        }

        public void Deactivate() {

        }

        public void Refresh() {

        }
        /*
        private void ConstructColorBar(List<CatagoryData> catagoryDatas) {
            int count = 0;
            foreach (CatagoryData catagoryData in catagoryDatas) {
                ColorBarElement newBar;
                if (count++ == 0)
                    newBar = Original;
                else
                    newBar = GameObject.Instantiate(original: Original, parent: Original.transform.parent.gameObject.transform) as ColorBarElement;
                newBar.Initialize(catagoryData);
                ColorBars.Add(newBar);
            }
            UpdateColorBar();
        }
        
        private void UpdateColorBar(List<ExpenseData> expenseDatas, decimal goal, float screenWidth) {
            float tempFloat = 0.00f;
            foreach (ColorBar colorBar in ColorBars) {
                colorBar.transform.localPosition = new Vector3(tempFloat, 0, 0);
                colorBar.SetWidth(GetWidthBasedOffPercentOfScreenWidth(ID: colorBar.ID, expenseDatas, goal, screenWidth);
                tempFloat += colorBar.GetWidth();
            }
        }

        private float GetWidthBasedOffPercentOfScreenWidth(int ID, List<ExpenseData> expenseDatas, decimal goal, float screenWidth) =>
            ((float)GetExpensesTotal(ID, expenseDatas) / (float)goal) * screenWidth;

        private decimal GetExpensesTotal(int id, List<ExpenseData> expenseDatas) {
            decimal total = 0.00m;
            foreach (ExpenseData expenseData in expenseDatas)
                if (expenseData.ID == id)
                    total += expenseData.Amount;
            return total;
        }
        */
    }
}