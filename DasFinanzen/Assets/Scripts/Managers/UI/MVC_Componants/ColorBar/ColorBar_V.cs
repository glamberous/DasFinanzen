﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBar_V : MonoBehaviour, IView {
        [SerializeField] private ColorBarElement OriginalColorBar = null;
        [SerializeField] private RectTransform CanvasRect = null;

        private ColorBar_HumbleView HumbleView = null;

        public void Activate() {

        }

        public void Refresh() {

        }

        public void Deactivate() {

        }
    }

    public class ColorBar_HumbleView {
        private List<ColorBarElement> ColorBarElements = new List<ColorBarElement>();
        private float ScreenWidth;

        public void ConstructView(ColorBar_ModelCollection modelCollection, ColorBarElement original, float screenWidth) {
            int count = 0;
            foreach (CatagoryModel catagoryModel in modelCollection.CatagoryModels) {
                ColorBarElement newBar;
                if (count++ == 0)
                    newBar = original;
                else
                    newBar = GameObject.Instantiate(original: original, parent: original.transform.parent.gameObject.transform) as ColorBarElement;
                ColorBarElements.Add(newBar);
            }
            ScreenWidth = screenWidth;
            UpdateColorBar(modelCollection);
        }

        private void UpdateColorBar(ColorBar_ModelCollection modelCollection) {
            Dictionary<int, decimal> ExpenseTotals = UIDataReformatter.GetExpenseTotals(modelCollection.CatagoryModels, modelCollection.ExpenseModels);
            Dictionary<int, CatagoryModel> CatagoryModelDict = UIDataReformatter.SortCatagoryModels(modelCollection.CatagoryModels);

            float fullWidth = 0.00f;
            foreach (ColorBarElement colorBarElement in ColorBarElements) {
                colorBarElement.transform.localPosition = new Vector3(fullWidth, 0, 0);
                float currentWidth = ((float)ExpenseTotals[colorBarElement.CatagoryID] / (float)modelCollection.Goal) * ScreenWidth;
                colorBarElement.UpdateView(CatagoryModelDict[colorBarElement.CatagoryID], currentWidth + 1);
                fullWidth += currentWidth;
            }
        }

        public void DeconstructView() {
            int count = 0;
            foreach (ColorBarElement colorBarElement in ColorBarElements) {
                if (0 == count++)
                    continue;
                GameObject.Destroy(colorBarElement.gameObject);
            }
        }
    }

    public class ColorBar_ModelCollection {
        public List<CatagoryModel> CatagoryModels = new List<CatagoryModel>();
        public List<ExpenseModel> ExpenseModels = new List<ExpenseModel>();
        public decimal Goal = 0.00m;
    }
}