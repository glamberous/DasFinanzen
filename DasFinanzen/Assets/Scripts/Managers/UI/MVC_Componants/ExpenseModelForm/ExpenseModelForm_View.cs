using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseModelForm_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI DateTextDisplay = null; // TODO Need to build a custom Date_InputField like the other fields.
        [SerializeField] private TextMeshProUGUI AmountTitle = null;

        [SerializeField] private Button SaveExpenseButton = null;
        [SerializeField] private Button DeleteExpenseButton = null;
        [SerializeField] private Currency_InputField CurrencyInputField = null;
        [SerializeField] private String_InputField StringInputField = null;
        [SerializeField] private Button EditDate = null;

        private ExpenseModelForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseModelForm_HumbleView();
            HumbleView.Awake(AmountTitle, CurrencyInputField, StringInputField, DateTextDisplay);

            ExpenseModelForm_Controller Controller = new ExpenseModelForm_Controller();
            SaveExpenseButton.SetController(Controller);
            DeleteExpenseButton.SetController(Controller);
            CurrencyInputField.SetController(Controller);
            StringInputField.SetController(Controller);
            EditDate.SetController(Controller);

            SaveExpenseButton.SetCommandID(0);
            DeleteExpenseButton.SetCommandID(1);
            CurrencyInputField.SetCommandID(2);
            StringInputField.SetCommandID(3);
            EditDate.SetCommandID(4);
        }

        public void Activate() {
            HumbleView.ConstructView(new ExpenseModelForm_ModelCollection(), DeleteExpenseButton);
            Messenger.AddListener(Events.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new ExpenseModelForm_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            Messenger.RemoveListener(Events.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Deactivated.");
        }
    }

    public class ExpenseModelForm_HumbleView {
        private TextMeshProUGUI AmountTitle = null;
        private Currency_InputField AmountInput = null;
        private String_InputField NameInput = null;
        private TextMeshProUGUI Date = null; // Problably need too build a custom InputField Script for this too.

        public void Awake(TextMeshProUGUI amountTitle, Currency_InputField amountInput, String_InputField nameInput, TextMeshProUGUI date) {
            AmountTitle = amountTitle;
            AmountInput = amountInput;
            NameInput = nameInput;
            Date = date;
        }

        public void ConstructView(ExpenseModelForm_ModelCollection modelCollection, Button deleteButton) {
            modelCollection.ExpenseModel.NameText = modelCollection.ExpenseModel.Catagory.NameText;
            deleteButton.gameObject.SetActive(modelCollection.ExpenseModel.ExpenseID != -1);
            RefreshView(modelCollection);
        }

        public void RefreshView(ExpenseModelForm_ModelCollection modelCollection) {
            AmountInput.SetDisplayText(modelCollection.ExpenseModel.Amount.ToString());
            NameInput.SetDisplayText(modelCollection.ExpenseModel.NameText);
            Date.text = modelCollection.ExpenseModel.Date.ToString("MM/dd");
            AmountTitle.text = modelCollection.Strings[22];
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
                case 4: PushEditDateWindow();break;
                default: Debug.Log($"[WARNING][ExpenseModelForm_Controller] CommandID {commandID} not recognized! ");  return;
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
        }

        private void SetName(string input) {
            Managers.Data.Runtime.TempExpenseModel.NameText = input;
        }

        private void PushEditDateWindow() {
            Managers.Data.Runtime.TempDay = Managers.Data.Runtime.TempExpenseModel.Date.Day;
            Managers.UI.Push(WINDOW.EDIT_DATE);
        }
    }

    public class ExpenseModelForm_ModelCollection {
        public ExpenseModel ExpenseModel = Managers.Data.Runtime.TempExpenseModel;
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 22 });
    }
}

