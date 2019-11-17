using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI {
    public class CatagoryList_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI HeaderText = null;
        [SerializeField] private CatagoryElement Original = null;
        [SerializeField] private TextMeshProUGUI SpentText = null;
        [SerializeField] private bool IsRecurring = false;

        private CatagoryList_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new CatagoryList_HumbleView();
            HumbleView.Awake(Original, HeaderText, SpentText, IsRecurring);
        }

        public void Activate() {
            HumbleView.ConstructView(new CatagoryList_ModelCollection());
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Messenger.AddListener(Localization.Events.LOCALE_CHANGED, Refresh);
            Debug.Log("CatagoryView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new CatagoryList_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            Messenger.RemoveListener(Localization.Events.LOCALE_CHANGED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("CatagoryView Deactivated.");
        }
    }

    public class CatagoryList_HumbleView {
        private const float CatagoryOffset = 20.0f;
        private float StartingTileHeight;
        private bool IsRecurring;
        private CatagoryElement[] CatagoryElements = new CatagoryElement[1];

        private TextMeshProUGUI Header = null;
        private TextMeshProUGUI Spent = null;

        public void Awake(CatagoryElement original, TextMeshProUGUI headerText, TextMeshProUGUI spentText, bool recurring) {
            CatagoryElements[0] = original;
            IsRecurring = recurring;

            Header = headerText;
            Spent = spentText;
        }

        public void ConstructView(CatagoryList_ModelCollection modelCollection) {
            CatagoryModel[] filteredCatagoryModels = GetFilteredCatagoryModels(modelCollection.CatagoryModels);
            decimal[] expenseTotals = GetExpenseTotals(filteredCatagoryModels, modelCollection.ExpenseModels);
            ResetOriginalElement(CatagoryElements[0], filteredCatagoryModels.Length);

            CatagoryElements[0].UpdateView(filteredCatagoryModels[0], expenseTotals[0]);
            for (int index = 1; index < CatagoryElements.Length; index++)
                CatagoryElements[index] = ConstructCatagoryElement(filteredCatagoryModels[index], index, expenseTotals[0]);

            Header.text = IsRecurring ? modelCollection.Strings[17] : modelCollection.Strings[16];
            Spent.text = modelCollection.Strings[18];
        }

        private CatagoryModel[] GetFilteredCatagoryModels(List<CatagoryModel> catagoryModels) {
            int count = MatchedIsRecurringCount(catagoryModels);
            CatagoryModel[] newCatagoryModels = new CatagoryModel[count];
            for (int index = 0; index < count; index++)
                newCatagoryModels[index] = catagoryModels[index];
            return newCatagoryModels;
        }

        private int MatchedIsRecurringCount(List<CatagoryModel> catagoryModels) {
            int count = 0;
            foreach (CatagoryModel catagoryModel in catagoryModels)
                if (catagoryModel.Recurring == IsRecurring)
                    count++;
            return count;
        }

        private decimal[] GetExpenseTotals(CatagoryModel[] catagoryModels, List<ExpenseModel> expenseModels) {
            decimal[] totals = new decimal[catagoryModels.Length];
            for (int index = 0; index < catagoryModels.Length; index++) {
                totals[index] = 0.00m;
                foreach (ExpenseModel expenseModel in expenseModels)
                    if (catagoryModels[index].CatagoryID == expenseModel.CatagoryID)
                        totals[index] += expenseModel.Amount;
            }
            return totals;
        }

        private void ResetOriginalElement(CatagoryElement original, int arrayLength) {
            CatagoryElements = new CatagoryElement[arrayLength];
            CatagoryElements[0] = original;
        }

        private CatagoryElement ConstructCatagoryElement(CatagoryModel model, int index, decimal total) {
            CatagoryElement newExpense = GameObject.Instantiate(original: CatagoryElements[index - 1], parent: CatagoryElements[index - 1].transform.parent.transform) as CatagoryElement;
            RectTransform newRect = newExpense.GetComponent<RectTransform>();
            newRect.anchoredPosition = new Vector3(newRect.anchoredPosition.x, newRect.anchoredPosition.y + CatagoryOffset);
            newExpense.SetAction(Controller.Instance.PushCatagoryWindow, model.CatagoryID);
            //newExpense.SetTileRect(TileRect); Add Later if I want the catagory tiles to scroll.
            newExpense.UpdateView(model, total);
            return newExpense;
        }

        public void RefreshView(CatagoryList_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            for (int index = 1; index < CatagoryElements.Length; index++)
                GameObject.Destroy(CatagoryElements[index].gameObject);
        }
    }

    public class CatagoryList_ModelCollection {
        public List<CatagoryModel> CatagoryModels = Managers.Data.FileData.CatagoryModels;
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 16, 17, 18 });
    }
}
