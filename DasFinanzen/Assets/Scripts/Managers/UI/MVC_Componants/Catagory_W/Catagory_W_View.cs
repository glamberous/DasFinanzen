using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Catagory_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Generic_Button BackButton = null;
        [SerializeField] private Generic_Button AddExpense = null;

    private Catagory_W_HumbleView HumbleView = null;
        public void Awake() {
            HumbleView = new Catagory_W_HumbleView();

            Catagory_W_Controller Controller = new Catagory_W_Controller();
            BackButton.SetController(Controller);
            AddExpense.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            BackButton.SetCommandID(0);
            AddExpense.SetCommandID(1);
        }

        public void Activate() {
            HumbleView.ConstructView(new Catagory_W_ModelCollection(), TitleText);
            //Add any Listeners needed here.
            Debug.Log("Catagory_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Catagory_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("Catagory_W_View Deactivated.");
        }
    }

    public class Catagory_W_HumbleView {
        private TextMeshProUGUI Title = null;

        public void ConstructView(Catagory_W_ModelCollection modelCollection, TextMeshProUGUI titleText) {
            Title = titleText;
            RefreshView(modelCollection);
        }

        public void RefreshView(Catagory_W_ModelCollection modelCollection) {
            Title.text = modelCollection.SelectedCatagory.NameText;
        }

        public void DeconstructView() {

        }
    }

    public class Catagory_W_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: BackButton();  break;
                case 1: AddExpense(); break;
                default: Debug.Log("[WARNING][Catagory_W_Controller] CommandID not recognized! "); return;
            }
        }

        private void BackButton() => Managers.UI.Pop();

        private void AddExpense() {
            Managers.Data.Runtime.TempExpenseModel = new ExpenseModel();
            Managers.UI.Push(WINDOW.EXPENSE);
        }
    }

    public class Catagory_W_ModelCollection {
        // Put Model Collections Here
        public CatagoryModel SelectedCatagory = DataReformatter.GetCatagoryModel(Managers.Data.FileData.CatagoryModels, Managers.Data.Runtime.CurrentCatagoryID);
    }
}
