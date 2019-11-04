using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseList_View : MonoBehaviour, IView {
        [SerializeField] private ExpenseElement Original = null;
        [SerializeField] private TextMeshProUGUI TransactionText = null;
        [SerializeField] private TextMeshProUGUI SpentText = null;

        private ExpenseList_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseList_HumbleView();
            HumbleView.Awake(Original, TransactionText, SpentText);
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
        private List<ExpenseElement> ExpenseElements = new List<ExpenseElement>();
        private ExpenseList_Controller Controller = new ExpenseList_Controller();
        private ExpenseElement Original = null;
        private TileUIData ExpenseTileUIData = null;
        private TextMeshProUGUI Transaction = null;
        private TextMeshProUGUI Spent = null;

        public void Awake(ExpenseElement original, TextMeshProUGUI transactionText, TextMeshProUGUI spentText) {
            Transaction = transactionText;
            Spent = spentText;
            Original = original;
            ExpenseTileUIData = new TileUIData(original.gameObject);
        }

        public void ConstructView(ExpenseList_ModelCollection modelCollection) {
            if (modelCollection.ExpenseModels.Count == 0)
                ConstructEmptyView();
            else
                ConstructRegularView(modelCollection);
        }

        private void ConstructEmptyView() {
            ExpenseTileUIData.Parent.SetActive(false);
            // Probably needs more Code here for unique UI (informative text) when Empty.
        }

        private void ConstructRegularView(ExpenseList_ModelCollection modelCollection) {
            ExpenseTileUIData.Parent.SetActive(true);
            foreach (ExpenseModel model in modelCollection.ExpenseModels)
                ExpenseElements.Add(ConstructExpenseElement(model));
            ExpenseTileUIData.UpdateTileSize(ExpenseElements.Count);

            Transaction.text = modelCollection.Strings[24];
            Spent.text = modelCollection.Strings[18];   
        }

        private ExpenseElement ConstructExpenseElement(ExpenseModel model) {
            ExpenseElement newExpense;
            if (ExpenseElements.Count == 0)
                newExpense = Original;
            else
                newExpense = GameObject.Instantiate(original: Original, parent: ExpenseTileUIData.Parent.transform) as ExpenseElement;
            newExpense.transform.localPosition = new Vector3(ExpenseTileUIData.StartPos.x, ExpenseTileUIData.StartPos.y - (Constants.CatagoryOffset * ExpenseElements.Count), ExpenseTileUIData.StartPos.z);
            newExpense.SetExpenseID(model.ExpenseID);
            newExpense.SetController(Controller);
            newExpense.SetCommandID(0);
            newExpense.UpdateView(model);
            return newExpense;
        }

        public void RefreshView(ExpenseList_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            int count = 0;
            foreach (ExpenseElement expenseElement in ExpenseElements)
                if (0 != count++)
                    GameObject.Destroy(expenseElement.gameObject);
            ExpenseElements = new List<ExpenseElement>();
        }
    }

    public class ExpenseList_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: ExpenseClicked(Convert.ToInt32(input)); break;
                default: Debug.Log("[WARNING][ExpenseList_Controller] CommandID not recognized! "); return;
            }
        }

        private void ExpenseClicked(int id) {
            Managers.Data.Runtime.TempExpenseModel = DataQueries.GetExpenseModel(Managers.Data.FileData.ExpenseModels, id);
            Managers.UI.Push(WINDOW.EXPENSE);
        }
    }

    public class ExpenseList_ModelCollection {
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime, Managers.Data.Runtime.CurrentCatagoryID);
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 24, 18 });
    }
}
