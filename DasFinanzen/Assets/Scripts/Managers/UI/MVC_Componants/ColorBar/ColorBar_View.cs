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
        }

        public void Activate() {
            HumbleView.ConstructView(new ColorBar_ModelCollection(), OriginalColorBar, CanvasRect.sizeDelta.x);
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(AppEvent.GOAL_UPDATED, Refresh);
        }

        public void Refresh() => HumbleView.Refresh(new ColorBar_ModelCollection);

        public void Deactivate() {
            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(AppEvent.GOAL_UPDATED, Refresh);
            HumbleView.DeconstructView();
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
            Refresh(modelCollection);
        }

        public void Refresh(ColorBar_ModelCollection modelCollection) {
            Dictionary<int, decimal> ExpenseTotals = DataReformatter.GetExpenseTotalsDict(modelCollection.CatagoryModels, modelCollection.ExpenseModels);
            Dictionary<int, CatagoryModel> CatagoryModelDict = DataReformatter.GetCatagoryModelsDict(modelCollection.CatagoryModels);

            float fullWidth = 0.00f;
            foreach (ColorBarElement colorBarElement in ColorBarElements) {
                colorBarElement.transform.localPosition = new Vector3(fullWidth, 0, 0);
                float currentWidth = ((float)ExpenseTotals[colorBarElement.CatagoryID] / (float)modelCollection.GoalModel.Amount) * ScreenWidth;
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

    public class ColorBar_ModelCollection : IModelCollection {
        public List<CatagoryModel> CatagoryModels = Managers.Data.FileData.CatagoryModels;
        public List<ExpenseModel> ExpenseModels = DataReformatter.FilterExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
        public GoalModel GoalModel = Managers.Data.FileData.GoalModel;
    }
}