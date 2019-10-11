using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    public class Catagory_V : MonoBehaviour, IView {
        [SerializeField] private CatagoryElement DailyOriginal = null;
        [SerializeField] private CatagoryElement MonthlyOriginal = null;

        private Catagory_M Model = null;
        private Catagory_C Controller = null;
        private Catagory_HumbleView HumbleView = null;

        public void Activate() {
            Model = new Catagory_M(Managers.Data.FileData, Managers.Data.EditorData);
            Controller = new Catagory_C(Model);
            HumbleView = new Catagory_HumbleView();

            Model.Catagories = HumbleView.ConstructView(DailyOriginal, MonthlyOriginal, Model.CatagoryDatas, Model.ExpenseDatas, this);
        }

        public void Deactivate() {
            Model = null;
            Controller = null;
            HumbleView = null;
        }

        public void Refresh() {
            HumbleView.Refresh(Model.ExpenseDatas);
        }
    }

    public class Catagory_HumbleView {
        public List<CatagoryElement> ConstructView(CatagoryElement dailyOriginal, CatagoryElement monthlyOriginal, List<CatagoryData> catagoryDatas, List<ExpenseData> expenseDatas, Catagory_V view) {
            List<CatagoryElement> newCatagories = new List<CatagoryElement>();
            TileUIData DailyUIData = new TileUIData(dailyOriginal.gameObject);
            TileUIData MonthlyUIData = new TileUIData(monthlyOriginal.gameObject);
            foreach (CatagoryData catagoryData in catagoryDatas)
                if (catagoryData.Reoccurring)
                    newCatagories.Add(ConstructCatagoryElement(catagoryData, MonthlyUIData));
                else
                    newCatagories.Add(ConstructCatagoryElement(catagoryData, DailyUIData));
            MonthlyUIData.UpdateTileSize();
            DailyUIData.UpdateTileSize();
            return newCatagories;
        }

        private CatagoryElement ConstructCatagoryElement(CatagoryData myCatagoryData, TileUIData UIData) {
            CatagoryElement newCatagory;
            if (UIData.Count == 0)
                newCatagory = UIData.Original.GetComponent<CatagoryElement>();
            else {
                newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryElement>(), parent: UIData.Parent.transform) as CatagoryElement;
                newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
            }
            newCatagory.Initialize(myCatagoryData);
            UIData.Count++;
            return newCatagory;
        }

        public void Refresh(List<ExpenseData> expenseDatas) {

        }
    }

    public class Catagory_C : IController {
        public Catagory_C(Catagory_M model) => Model = model;
        private Catagory_M Model = null;

        public void Close() => Managers.UI.Pop();

        public void OpenCatagory(int id) {
            Model.CurrentCatagoryID = id;
            Managers.UI.Push(WINDOW.CATAGORY);
        }
    }

    public class Catagory_M : IModel {
        public Catagory_M(FileData fileData, EditorData editorData) {
            CatagoryDatas = editorData.CatagoryDatas;
            CurrentCatagoryID = editorData.CurrentCatagoryID;

            ExpenseDatas = fileData.ExpenseDatas;
        }

        public List<ExpenseData> ExpenseDatas = null;
        public List<CatagoryData> CatagoryDatas = null;
        public int CurrentCatagoryID = -1;
        public List<CatagoryElement> Catagories = null;
    }
}
