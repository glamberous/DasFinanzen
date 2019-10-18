using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Catagory_View : MonoBehaviour, IView {
        [SerializeField] private CatagoryElement DailyOriginal = null;
        [SerializeField] private CatagoryElement MonthlyOriginal = null;

        private Catagory_Controller Controller = null;
        private Catagory_HumbleView HumbleView = null;

        public void Awake() {
            Controller = new Catagory_Controller();
            HumbleView = new Catagory_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(Managers.Data.UIModelCollector.GetCatagory(), DailyOriginal, MonthlyOriginal);
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
        }

        public void Refresh() {
            HumbleView.Refresh(Managers.Data.UIModelCollector.GetCatagory());
        }

        public void Deactivate() {
            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, Refresh);
            HumbleView.DeconstructView();
        }
    }

    public class Catagory_HumbleView {
        private List<CatagoryElement> CatagoryElements = new List<CatagoryElement>();

        public void ConstructView(Catagory_ModelCollection ModelCollection, CatagoryElement dailyOriginal, CatagoryElement monthlyOriginal) {
            TileUIData DailyUIData = new TileUIData(dailyOriginal.gameObject);
            TileUIData MonthlyUIData = new TileUIData(monthlyOriginal.gameObject);
            foreach (CatagoryModel catagoryModel in ModelCollection.CatagoryModels)
                if (catagoryModel.Reoccurring)
                    CatagoryElements.Add(ConstructCatagoryElement(catagoryModel, MonthlyUIData));
                else
                    CatagoryElements.Add(ConstructCatagoryElement(catagoryModel, DailyUIData));
            MonthlyUIData.UpdateTileSize();
            DailyUIData.UpdateTileSize();
            Refresh(ModelCollection);
        }

        private CatagoryElement ConstructCatagoryElement(CatagoryModel myCatagoryModel, TileUIData UIData) {
            CatagoryElement newCatagory;
            if (UIData.Count == 0)
                newCatagory = UIData.Original.GetComponent<CatagoryElement>();
            else {
                newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryElement>(), parent: UIData.Parent.transform) as CatagoryElement;
                newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
            }
            UIData.Count++;
            return newCatagory;
        }

        public void Refresh(Catagory_ModelCollection modelCollection) {
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

    public class Catagory_Controller : IController {
        public void Close() => Managers.UI.Pop();

        public void OpenCatagory(int id) {
            Managers.UI.Push(WINDOW.CATAGORY);
        }
    }

    public class Catagory_ModelCollection {
        public List<CatagoryModel> CatagoryModels = new List<CatagoryModel>();
        public List<ExpenseModel> ExpenseModels = new List<ExpenseModel>();
    }
}
