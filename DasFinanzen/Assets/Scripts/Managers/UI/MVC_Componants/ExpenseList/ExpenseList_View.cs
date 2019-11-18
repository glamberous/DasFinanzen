using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseList_View : MonoBehaviour, IView {
        [SerializeField] private ExpenseElement Original = null;
        [SerializeField] private RectTransform TileRect = null;
        [SerializeField] private TextMeshProUGUI TransactionText = null;
        [SerializeField] private TextMeshProUGUI SpentText = null;

        private ExpenseList_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseList_HumbleView();
            HumbleView.Awake(Original, TileRect, TransactionText, SpentText);
        }

        public void Activate() {
            HumbleView.ConstructView(new ExpenseList_ModelCollection());
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("ExpenseView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new ExpenseList_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("ExpenseView Deactivated.");
        }
    }

    public class ExpenseList_HumbleView {
        private const float CatagoryOffset = 20f;
        private float StartingTileHeight;
        private ExpenseElement[] ExpenseElements = new ExpenseElement[1];
        private RectTransform TileRect = null;
        
        private TextMeshProUGUI Transaction = null;
        private TextMeshProUGUI Spent = null;

        public void Awake(ExpenseElement original, RectTransform tileRect, TextMeshProUGUI transactionText, TextMeshProUGUI spentText) {
            ExpenseElements[0] = original;
            TileRect = tileRect;
            StartingTileHeight = TileRect.sizeDelta.y;

            Transaction = transactionText;
            Spent = spentText;
        }

        public void ConstructView(ExpenseList_ModelCollection modelCollection) {
            if (modelCollection.ExpenseModels.Count == 0)
                ConstructEmptyView();
            else 
                ConstructRegularView(modelCollection);
        }

        private void ConstructEmptyView() {
            TileRect.gameObject.SetActive(false);
            // Probably needs more Code here for unique UI (informative text) when Empty.
        }

        private void ConstructRegularView(ExpenseList_ModelCollection modelCollection) {
            ResetOriginalElement(ExpenseElements[0], modelCollection.ExpenseModels.Count);
            TileRect.gameObject.SetActive(true);
            ExpenseElements[0].UpdateView(modelCollection.ExpenseModels[0]);
            for (int index = 1; index < modelCollection.ExpenseModels.Count; index++)
                ExpenseElements[index] = ConstructExpenseElement(modelCollection.ExpenseModels[index], index);
            TileRect.sizeDelta = new Vector2(TileRect.sizeDelta.x, StartingTileHeight + ((modelCollection.ExpenseModels.Count - 1) * CatagoryOffset));

            Transaction.text = modelCollection.Strings[24];
            Spent.text = modelCollection.Strings[18];
        }

        private void ResetOriginalElement(ExpenseElement original, int arrayLength) {
            ExpenseElements = new ExpenseElement[arrayLength];
            ExpenseElements[0] = original;
        }

        private ExpenseElement ConstructExpenseElement(ExpenseModel model, int index) {
            ExpenseElement newExpense = GameObject.Instantiate(original: ExpenseElements[index-1], parent: ExpenseElements[index-1].transform.parent.transform) as ExpenseElement;
            RectTransform newRect = newExpense.GetComponent<RectTransform>();
            newRect.anchoredPosition = new Vector3(newRect.anchoredPosition.x, newRect.anchoredPosition.y - CatagoryOffset);
            newExpense.UpdateView(model);
            return newExpense;
        }

        public void RefreshView(ExpenseList_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            for (int index = 1; index < ExpenseElements.Length; index++)
                GameObject.Destroy(ExpenseElements[index].gameObject);
        }
    }

    public class ExpenseList_ModelCollection {
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime, Managers.Data.Runtime.CurrentCatagoryID);
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 24, 18 });
    }
}
