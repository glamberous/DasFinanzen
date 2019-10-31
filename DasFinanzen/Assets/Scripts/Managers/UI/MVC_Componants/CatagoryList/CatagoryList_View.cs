using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI {
    public class CatagoryList_View : MonoBehaviour, IView {
        [SerializeField] private CatagoryElement DailyOriginal = null;
        [SerializeField] private CatagoryElement MonthlyOriginal = null;

        private CatagoryList_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new CatagoryList_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(new CatagoryList_ModelCollection(), DailyOriginal, MonthlyOriginal);
            Messenger.AddListener(UIEvent.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(UIEvent.MONTH_CHANGED, Refresh);
            Debug.Log("CatagoryView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new CatagoryList_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(UIEvent.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(UIEvent.MONTH_CHANGED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("CatagoryView Deactivated.");
        }
    }

    public class CatagoryList_HumbleView {
        private List<CatagoryElement> RecurringCatagoryElements = new List<CatagoryElement>();
        private List<CatagoryElement> DailyCatagoryElements = new List<CatagoryElement>();

        public void ConstructView(CatagoryList_ModelCollection ModelCollection, CatagoryElement dailyOriginal, CatagoryElement monthlyOriginal) {
            CatagoryList_Controller Controller = new CatagoryList_Controller();
            TileUIData DailyUIData = new TileUIData(dailyOriginal.gameObject);
            TileUIData MonthlyUIData = new TileUIData(monthlyOriginal.gameObject);
            foreach (CatagoryModel catagoryModel in ModelCollection.CatagoryModels)
                if (catagoryModel.Recurring)
                    RecurringCatagoryElements.Add(ConstructCatagoryElement(catagoryModel, MonthlyUIData, RecurringCatagoryElements.Count, Controller));
                else
                    DailyCatagoryElements.Add(ConstructCatagoryElement(catagoryModel, DailyUIData, DailyCatagoryElements.Count, Controller));
            MonthlyUIData.UpdateTileSize(RecurringCatagoryElements.Count);
            DailyUIData.UpdateTileSize(DailyCatagoryElements.Count);
            RefreshView(ModelCollection);
        }

        private CatagoryElement ConstructCatagoryElement(CatagoryModel myCatagoryModel, TileUIData UIData, int count, CatagoryList_Controller controller) {
            CatagoryElement newCatagory;
            if (count == 0)
                newCatagory = UIData.Original.GetComponent<CatagoryElement>();
            else
                newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryElement>(), parent: UIData.Parent.transform) as CatagoryElement;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * count), UIData.StartPos.z);
            newCatagory.SetController(controller);
            newCatagory.SetCommandID(0);
            newCatagory.SetCatagoryID(myCatagoryModel.CatagoryID);
            return newCatagory;
        }

        public void RefreshView(CatagoryList_ModelCollection modelCollection) {
            Dictionary<int, decimal> ExpenseTotalsDict = DataReformatter.GetExpenseTotalsDict(modelCollection.CatagoryModels, modelCollection.ExpenseModels);
            Dictionary<int, CatagoryModel> CatagoryModelDict = DataReformatter.GetCatagoryModelsDict(modelCollection.CatagoryModels);

            foreach (CatagoryElement catagoryElem in RecurringCatagoryElements)
                catagoryElem.UpdateView(CatagoryModelDict[catagoryElem.CatagoryID], ExpenseTotalsDict[catagoryElem.CatagoryID]);
            foreach (CatagoryElement catagoryElem in DailyCatagoryElements)
                catagoryElem.UpdateView(CatagoryModelDict[catagoryElem.CatagoryID], ExpenseTotalsDict[catagoryElem.CatagoryID]);
        }

        public void DeconstructView() {
            DestroyCatagoryElements(RecurringCatagoryElements);
            DestroyCatagoryElements(DailyCatagoryElements);
        }

        private void DestroyCatagoryElements(List<CatagoryElement> catagoryElements) {
            int count = 0;
            foreach (CatagoryElement catagoryElement in catagoryElements) {
                RecurringCatagoryElements.Remove(catagoryElement);
                if (count++ != 0)
                    GameObject.Destroy(catagoryElement.gameObject);
            }
        }
    }

    public class CatagoryList_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch(commandID) {
                case 0: CatagoryClicked(Convert.ToInt32(input)); break;
                default: Debug.Log("[WARNING][CatagoryList_Controller] CommandID not recognized! "); return;
            }
        }

        private void CatagoryClicked(int id) {
            Managers.Data.Runtime.CurrentCatagoryID = id;
            Managers.UI.Push(WINDOW.CATAGORY);
        }
    }

    public class CatagoryList_ModelCollection {
        public List<CatagoryModel> CatagoryModels = Managers.Data.FileData.CatagoryModels;
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}
