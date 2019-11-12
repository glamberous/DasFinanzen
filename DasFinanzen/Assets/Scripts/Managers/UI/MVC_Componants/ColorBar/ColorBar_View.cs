using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBar_View : MonoBehaviour, IView {
        [SerializeField] private ColorBarElement OriginalColorBar = null;
        [SerializeField] private RectTransform CanvasRect = null;

        private ColorBar_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ColorBar_HumbleView();
            HumbleView.Awake(CanvasRect.sizeDelta.x);
            Debug.Log("Canvas Width: " + CanvasRect.sizeDelta.x.ToString());
            Debug.Log("Screen Width: " + Screen.width.ToString());
        }

        public void Activate() {
            HumbleView.ConstructView(new ColorBar_ModelCollection(), OriginalColorBar);
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.GOAL_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("ColorBarView Activated.");
        }

        public void Refresh() => HumbleView.Refresh(new ColorBar_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.GOAL_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("ColorBarView Deactivated.");
        }
    }

    public class ColorBar_HumbleView {
        private List<ColorBarElement> ColorBarElements = new List<ColorBarElement>();
        private float ScreenWidth;

        public void Awake(float screenWidth) {
            ScreenWidth = screenWidth;
        }

        public void ConstructView(ColorBar_ModelCollection modelCollection, ColorBarElement original) {
            int count = 0;
            foreach (CatagoryModel catagoryModel in modelCollection.CatagoryModels) {
                ColorBarElement newBar;
                if (count++ == 0)
                    newBar = original;
                else 
                    newBar = GameObject.Instantiate<ColorBarElement>(original: original, parent: original.transform.parent.gameObject.transform);
                newBar.SetID(catagoryModel.CatagoryID);
                ColorBarElements.Add(newBar);
            }
            Refresh(modelCollection);
        }

        public void Refresh(ColorBar_ModelCollection modelCollection) {
            Dictionary<int, decimal> ExpenseTotals = DataReformatter.GetExpenseTotalsDict(modelCollection.CatagoryModels, modelCollection.ExpenseModels);
            Dictionary<int, CatagoryModel> CatagoryModelDict = DataReformatter.GetCatagoryModelsDict(modelCollection.CatagoryModels);

            float fullWidth = 0.00f;
            foreach (ColorBarElement colorBarElement in ColorBarElements) {
                colorBarElement.transform.localPosition = new Vector3(fullWidth, 0, 0);
                float currentWidth = ((float)ExpenseTotals[colorBarElement.CatagoryID] / (float)modelCollection.GoalModel.Amount) * ScreenWidth;
                // Fixes a float inaccuracy bug where a sliver of color for catagories that have no expenses was showing.
                currentWidth = currentWidth >= 1 ? currentWidth : 0;
                colorBarElement.UpdateView(CatagoryModelDict[colorBarElement.CatagoryID], currentWidth);
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

    public class ColorBar_ModelCollection : IModelCollection {
        public List<CatagoryModel> CatagoryModels = Managers.Data.FileData.CatagoryModels;
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
    }
}