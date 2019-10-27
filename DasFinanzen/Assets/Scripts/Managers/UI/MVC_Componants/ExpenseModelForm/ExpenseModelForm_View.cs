using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseModelForm_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI AmountTextDisplay = null;
        [SerializeField] private TextMeshProUGUI NameTextDisplay = null;
        [SerializeField] private TextMeshProUGUI DateTextDisplay = null;

        [SerializeField] private Generic_Button SaveExpenseButton = null;
        [SerializeField] private Generic_Button DeleteExpenseButton = null;
        [SerializeField] private Currency_InputField CurrencyInputField = null;
        [SerializeField] private String_InputField StringInputField = null;

        private ExpenseModelForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseModelForm_HumbleView();

            ExpenseModelForm_Controller Controller = new ExpenseModelForm_Controller();
            SaveExpenseButton.SetController(Controller);
            DeleteExpenseButton.SetController(Controller);
            CurrencyInputField.SetController(Controller);
            StringInputField.SetController(Controller);

            SaveExpenseButton.SetCommandID(0);
            DeleteExpenseButton.SetCommandID(1);
            CurrencyInputField.SetCommandID(2);
            StringInputField.SetCommandID(3);
        }

        public void Activate() {
            HumbleView.ConstructView(new ExpenseModelForm_ModelCollection(), AmountTextDisplay, NameTextDisplay, DateTextDisplay, DeleteExpenseButton);
            Messenger.AddListener(UIEvent.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new ExpenseModelForm_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            Messenger.RemoveListener(UIEvent.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Deactivated.");
        }
    }

    public class ExpenseModelForm_HumbleView {
        private TextMeshProUGUI Amount = null;
        private TextMeshProUGUI Name = null;
        private TextMeshProUGUI Date = null;

        public void ConstructView(ExpenseModelForm_ModelCollection modelCollection, TextMeshProUGUI amount, TextMeshProUGUI name, TextMeshProUGUI date, Generic_Button deleteButton) {
            Amount = amount;
            Name = name;
            Date = date;

            deleteButton.gameObject.SetActive(IDTracker.IsNew(IDType.EXPENSE, modelCollection.ExpenseModel.ExpenseID));
            RefreshView(modelCollection);
        }

        public void RefreshView(ExpenseModelForm_ModelCollection modelCollection) {
            Amount.text = modelCollection.ExpenseModel.Amount.ToString();
            Name.text = modelCollection.ExpenseModel.NameText;
            Date.text = modelCollection.ExpenseModel.Date.ToString("mm/dd");
        }

        public void DeconstructView() {
            
        }
    }

    public class ExpenseModelForm_Controller : IController { 
        public void TriggerCommand(int commandID, string input = null) {
            switch (commandID) {
                case 0: SaveExpense(); break;
                case 1: DeleteExpense(); break;
                case 2: SetAmount(input); break;
                case 3: SetName(input); break;
                default: Debug.Log("[WARNING][ExpenseModelForm_Controller] CommandID not recognized! ");  return;
            }
        }

        private void SaveExpense() {
            Managers.Data.Runtime.TempExpenseModel.Save();
            Managers.Data.Runtime.TempExpenseModel = null;
            Managers.UI.Pop();
        }

        private void DeleteExpense() {
            Managers.Data.Runtime.TempExpenseModel.Delete();
            Managers.Data.Runtime.TempExpenseModel = null;
            Managers.UI.Pop();
        }

        private void SetAmount(string input) {
            Managers.Data.Runtime.TempExpenseModel.Amount = DataReformatter.ConvertStringToDecimal(input);
            Messenger.Broadcast(UIEvent.TEMP_EXPENSE_UPDATED);
        }

        private void SetName(string input) {
            Managers.Data.Runtime.TempExpenseModel.NameText = input;
            Messenger.Broadcast(UIEvent.TEMP_EXPENSE_UPDATED);
        }
    }

    public class ExpenseModelForm_ModelCollection {
        public ExpenseModel ExpenseModel = Managers.Data.Runtime.TempExpenseModel;
    }
}

