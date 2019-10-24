using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Expense_View : MonoBehaviour, IView {
        [SerializeField] private ExpenseElement Original = null;

        private Expense_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Expense_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(new Expense_ModelCollection(), Original);
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Debug.Log("ExpenseView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Expense_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("ExpenseView Deactivated.");
        }
    }

    public class Expense_HumbleView {
        private TileUIData ExpenseTileUIData = null;
        private ExpenseElement Original = null;

        public void ConstructView(Expense_ModelCollection modelCollection, ExpenseElement original) {
            Original = original;
            ExpenseTileUIData = new TileUIData(original.gameObject);
            if (modelCollection.ExpenseModels.Count != 0)
                ConstructRegularView();
            else
                InitializeEmptyExpenseView();
        }

        private void ConstructRegularView() {
            foreach (ExpenseData data in Managers.Data.CurrentExpenseDatas)
                InitializeExpense(data);
            ExpenseUIData.UpdateTileSize();
        }

        private void InitializeEmptyExpenseView() {
            ExpenseUIData.Parent.SetActive(false);
            // Probably needs more Code here for unique UI when Empty.
        }

        private void InitializeExpense(ExpenseData myExpenseData) {
            Expense newExpense;
            if (ExpenseUIData.Count == 0)
                newExpense = ExpenseOriginal;
            else
                newExpense = GameObject.Instantiate(original: ExpenseOriginal, parent: ExpenseUIData.Parent.transform) as Expense;
            newExpense.transform.localPosition = new Vector3(ExpenseUIData.StartPos.x, ExpenseUIData.StartPos.y - (Constants.CatagoryOffset * ExpenseUIData.Count), ExpenseUIData.StartPos.z);
            newExpense.Construct(myExpenseData);
            ExpenseUIs.Add(newExpense);
            ExpenseUIData.Count++;
        }

        public void RefreshView(Expense_ModelCollection modelCollection) {
            if (modelCollection.ExpenseModels.Count != 0)
                InitializeRegularExpenseView();
            else
                Original.gameObject.SetActive(false);
        }

        public void DeconstructView() {
            ExpenseTileUIData.Count = 0;
            int count = 0;
            foreach (ExpenseElement expenseElement in ExpenseElements) {
                if (0 == count++)
                    continue;
                GameObject.Destroy(expenseElement.gameObject);
            }
        }
    }

    public class Expense_ModelCollection {
        public CatagoryModel CurrentCatagory = DataReformatter.GetCurrentCatagoryModel(Managers.Data.FileData.CatagoryModels, Managers.Data.Runtime.CurrentCatagoryID);
        public List<ExpenseModel> ExpenseModels = DataReformatter.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime, Managers.Data.Runtime.CurrentCatagoryID);
    }
}
