using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI {
    public class ExpenseList_View : MonoBehaviour, IView {
        [SerializeField] private ExpenseElement Original = null;

        private ExpenseList_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseList_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(new ExpenseList_ModelCollection(), Original);
            Messenger.AddListener(UIEvent.EXPENSES_UPDATED, Refresh);
            Debug.Log("ExpenseView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new ExpenseList_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(UIEvent.EXPENSES_UPDATED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("ExpenseView Deactivated.");
        }
    }

    public class ExpenseList_HumbleView {
        private List<ExpenseElement> ExpenseElements = new List<ExpenseElement>();
        private TileUIData ExpenseTileUIData = null;

        public void ConstructView(ExpenseList_ModelCollection modelCollection, ExpenseElement original) {
            if (modelCollection.ExpenseModels.Count == 0)
                ConstructEmptyView();
            else
                ConstructRegularView(modelCollection, original);
        }

        private void ConstructEmptyView() {
            ExpenseTileUIData.Parent.SetActive(false);
            // Probably needs more Code here for unique UI (informative text) when Empty.
        }

        private void ConstructRegularView(ExpenseList_ModelCollection modelCollection, ExpenseElement original) {
            foreach (ExpenseModel model in modelCollection.ExpenseModels)
                ExpenseElements.Add(ConstructExpenseElement(model, original));
            ExpenseTileUIData.UpdateTileSize(ExpenseElements.Count);
        }

        private ExpenseElement ConstructExpenseElement(ExpenseModel model, ExpenseElement original) {
            ExpenseElement newExpense;
            if (ExpenseElements.Count == 0)
                newExpense = original;
            else
                newExpense = GameObject.Instantiate(original: original, parent: ExpenseTileUIData.Parent.transform) as ExpenseElement;
            newExpense.transform.localPosition = new Vector3(ExpenseTileUIData.StartPos.x, ExpenseTileUIData.StartPos.y - (Constants.CatagoryOffset * ExpenseElements.Count), ExpenseTileUIData.StartPos.z);
            newExpense.SetID(model.ExpenseID);
            newExpense.UpdateView(model);
            return newExpense;
        }

        public void RefreshView(ExpenseList_ModelCollection modelCollection) {
            if (modelCollection.ExpenseModels.Count == 0)
                ConstructEmptyView();
            else {
                ExpenseTileUIData.Parent.SetActive(true);
                if (ExpenseElements.Count < modelCollection.ExpenseModels.Count)
                    AddExpenseElement(modelCollection);
                else if (ExpenseElements.Count > modelCollection.ExpenseModels.Count)
                    RemoveExpenseElement(modelCollection);
                else
                    RefreshExpenseElements(modelCollection);
                ExpenseTileUIData.UpdateTileSize(ExpenseElements.Count);
            }
        }

        private void AddExpenseElement(ExpenseList_ModelCollection modelCollection) =>
            ExpenseElements.Add(ConstructExpenseElement(modelCollection.ExpenseModels.Last(), ExpenseElements.First()));

        private void RemoveExpenseElement(ExpenseList_ModelCollection modelCollection) {
            Dictionary<int, ExpenseModel> ExpenseModelsDict = DataReformatter.GetExpenseModelsDict(modelCollection.ExpenseModels);

            foreach (ExpenseElement expenseElem in ExpenseElements) {
                if (!ExpenseModelsDict.ContainsKey(expenseElem.ExpenseID)) {
                    ExpenseElement ExpenseToDelete = expenseElem;
                    ExpenseElements.Remove(expenseElem);
                    GameObject.Destroy(ExpenseToDelete);
                    return;
                }
            }
        }

        private void RefreshExpenseElements(ExpenseList_ModelCollection modelCollection) {
            Dictionary<int, ExpenseModel> ExpenseModelsDict = DataReformatter.GetExpenseModelsDict(modelCollection.ExpenseModels);

            foreach (ExpenseElement expenseElem in ExpenseElements)
                expenseElem.UpdateView(ExpenseModelsDict[expenseElem.ExpenseID]);
        }

        public void DeconstructView() {
            int count = 0;
            foreach (ExpenseElement expenseElement in ExpenseElements) {
                ExpenseElements.Remove(expenseElement);
                if (count++ != 0)
                    GameObject.Destroy(expenseElement.gameObject);
                }
            }
        }

    public static class ExpenseList_Controller {
        public static void EditExpenseClicked(int id) {
            Managers.Data.Runtime.CurrentExpenseID = id;
            Managers.UI.Push(WINDOW.EXPENSE);
        }
        
        public static void AddExpenseClicked() {
            Managers.Data.Runtime.CurrentExpenseID = -1;
            Managers.UI.Push(WINDOW.EXPENSE);
        }

        public static void Close() => Managers.UI.Pop();
    }

    public class ExpenseList_ModelCollection {
        public List<ExpenseModel> ExpenseModels = DataReformatter.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime, Managers.Data.Runtime.CurrentCatagoryID);
    }
}
