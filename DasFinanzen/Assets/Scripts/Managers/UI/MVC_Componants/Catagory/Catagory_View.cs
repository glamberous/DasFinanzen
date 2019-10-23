using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Catagory_View : MonoBehaviour, IView {
        [SerializeField] private CatagoryElement DailyOriginal = null;
        [SerializeField] private CatagoryElement MonthlyOriginal = null;

        private Catagory_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Catagory_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(new Catagory_ModelCollection(), DailyOriginal, MonthlyOriginal);
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Debug.Log("CatagoryView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Catagory_ModelCollection());

        public void Deactivate() {
            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, Refresh);
            HumbleView.DeconstructView();
            Debug.Log("CatagoryView Deactivated.");
        }
    }

    public class Catagory_HumbleView {
        private List<CatagoryElement> CatagoryElements = new List<CatagoryElement>();

        public void ConstructView(Catagory_ModelCollection ModelCollection, CatagoryElement dailyOriginal, CatagoryElement monthlyOriginal) {
            TileUIData DailyUIData = new TileUIData(dailyOriginal.gameObject);
            TileUIData MonthlyUIData = new TileUIData(monthlyOriginal.gameObject);
            foreach (CatagoryModel catagoryModel in ModelCollection.CatagoryModels)
                if (catagoryModel.Recurring)
                    CatagoryElements.Add(ConstructCatagoryElement(catagoryModel, MonthlyUIData));
                else
                    CatagoryElements.Add(ConstructCatagoryElement(catagoryModel, DailyUIData));
            MonthlyUIData.UpdateTileSize();
            DailyUIData.UpdateTileSize();
            RefreshView(ModelCollection);
        }

        private CatagoryElement ConstructCatagoryElement(CatagoryModel myCatagoryModel, TileUIData UIData) {
            CatagoryElement newCatagory;
            if (UIData.Count == 0)
                newCatagory = UIData.Original.GetComponent<CatagoryElement>();
            else 
                newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryElement>(), parent: UIData.Parent.transform) as CatagoryElement;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
            newCatagory.SetID(myCatagoryModel.CatagoryID);
            UIData.Count++;
            return newCatagory;
        }

        public void RefreshView(Catagory_ModelCollection modelCollection) {
            Dictionary<int, decimal> ExpenseTotals = DataReformatter.GetExpenseTotalsDict(modelCollection.CatagoryModels, modelCollection.ExpenseModels);
            Dictionary<int, CatagoryModel> CatagoryModelDict = DataReformatter.GetCatagoryModelsDict(modelCollection.CatagoryModels);

            foreach (CatagoryElement catagoryElem in CatagoryElements)
                catagoryElem.UpdateView(CatagoryModelDict[catagoryElem.CatagoryID], ExpenseTotals[catagoryElem.CatagoryID]);
        }

        public void DeconstructView() {
            int count = 0;
            foreach (CatagoryElement catagoryElement in CatagoryElements) {
                if (0 == count++)
                    continue;
                GameObject.Destroy(catagoryElement.gameObject);
            }
        }
    }

    public static class Catagory_Controller {
        public static void CatagoryClicked(int id) {
            Managers.Data.Runtime.CurrentCatagoryID = id;
            Managers.UI.Push(WINDOW.CATAGORY);
        }
    }

    public class Catagory_ModelCollection {
        public List<CatagoryModel> CatagoryModels = Managers.Data.FileData.CatagoryModels;
        public List<ExpenseModel> ExpenseModels = DataReformatter.FilterExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}
