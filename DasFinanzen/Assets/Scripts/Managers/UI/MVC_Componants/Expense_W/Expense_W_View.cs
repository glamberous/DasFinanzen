using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class Expense_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button BackButton = null;

        private Expense_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Expense_W_HumbleView();
            HumbleView.Awake(TitleText);

            Expense_W_Controller Controller = new Expense_W_Controller();
            BackButton.SetController(Controller);
            BackButton.SetCommandID(0);
        }

        public void Activate() {
            HumbleView.ConstructView(new Expense_W_ModelCollection());
            Debug.Log("Expense_W_View Activated.");
        }

        public void Refresh() {
            HumbleView.RefreshView(new Expense_W_ModelCollection());
        }

        public void Deactivate() {
            Debug.Log("Expense_W_View Deactivated.");
        }
    }

    public class Expense_W_HumbleView {
        private TextMeshProUGUI PageTitle = null;

        public void Awake(TextMeshProUGUI title) {
            PageTitle = title;
        }

        public void ConstructView(Expense_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Expense_W_ModelCollection modelCollection) {
            PageTitle.text = IDTracker.IsNew(modelCollection.ExpenseModel.ExpenseID) ? modelCollection.Strings[23] : modelCollection.Strings[21];
        }

        public void DeconstructView() {

        }
    }

    public class Expense_W_Controller : IController {
        public void TriggerCommand(int commandID, string input = null) {
            switch (commandID) {
                case 0: BackButton(); break;
                default: Debug.Log("[WARNING][Expense_W_Controller] CommandID not recognized! "); return;
            }
        }

        private void BackButton() {
            Managers.Data.Runtime.TempExpenseModel = null;
            Managers.UI.Pop();
        }
    }

    public class Expense_W_ModelCollection {
        public ExpenseModel ExpenseModel = Managers.Data.Runtime.TempExpenseModel;
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 21, 23 });
    }
}

