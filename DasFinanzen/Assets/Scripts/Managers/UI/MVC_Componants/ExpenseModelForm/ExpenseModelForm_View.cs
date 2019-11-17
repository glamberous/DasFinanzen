using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseModelForm_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI DateTextDisplay = null; // TODO Need to build a custom Date_InputField like the other fields.
        [SerializeField] private TextMeshProUGUI AmountTitle = null;

        [SerializeField] private Button_Void SaveExpenseButton = null;
        [SerializeField] private Button_Void DeleteExpenseButton = null;
        [SerializeField] private Currency_InputField CurrencyInputField = null;
        [SerializeField] private String_InputField StringInputField = null;
        [SerializeField] private Button_Void EditDateButton = null;

        private ExpenseModelForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new ExpenseModelForm_HumbleView();
            HumbleView.Awake(AmountTitle, CurrencyInputField, StringInputField, DateTextDisplay);

            SaveExpenseButton.SetAction(Controller.Instance.SaveTempExpense);
            DeleteExpenseButton.SetAction(Controller.Instance.DeleteTempExpense);
            EditDateButton.SetAction(Controller.Instance.PushEditDateWindow);
            StringInputField.SetAction(Controller.Instance.SetTempExpenseName);

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

        public void ConstructView(ExpenseModelForm_ModelCollection modelCollection, Button_Void deleteButton) {
            if (modelCollection.ExpenseModel.ExpenseID == -1) {
                modelCollection.ExpenseModel.NameText = modelCollection.ExpenseModel.Catagory.NameText;
                deleteButton.gameObject.SetActive(false);
            } 
            else
                deleteButton.gameObject.SetActive(true);
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

    public class ExpenseModelForm_ModelCollection {
        public ExpenseModel ExpenseModel = Managers.Data.Runtime.TempExpenseModel;
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 22 });
    }
}

