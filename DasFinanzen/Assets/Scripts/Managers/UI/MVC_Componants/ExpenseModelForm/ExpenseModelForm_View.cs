using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    [RequireComponent(typeof(ExpenseModelForm_Controller))]
    public class ExpenseModelForm_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private TextMeshProUGUI AmountText = null;
        [SerializeField] private TextMeshProUGUI NameText = null;
        [SerializeField] private TextMeshProUGUI DateText = null;

        private ExpenseModel Model = null;
        private ExpenseModelForm_Controller Controller = null;

        public void Awake() {
            Controller = GetComponent<ExpenseModelForm_Controller>();
        }

        public void Activate() {
            Model = Managers.Data.Runtime.CurrentExpenseID == -1 ? new ExpenseModel() : DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.CurrentExpenseID);
            Controller.SetModel(Model);

            Refresh();
            Messenger.AddListener(UIEvent.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Activated.");
        }

        public void Refresh() {
            TitleText.text = IDTracker.IsNew(IDType.EXPENSE, Model.ExpenseID) ? "Add Expense" : "Edit Expense";
            AmountText.text = Model.Amount.ToString();
            NameText.text = Model.NameText;
            DateText.text = Model.Date.ToString("mm/dd");
        }

        public void Deactivate() {
            Messenger.RemoveListener(UIEvent.TEMP_EXPENSE_UPDATED, Refresh);
            Debug.Log("ExpenseDataEntryView Deactivated.");
        }
    }

    public class ExpenseModelForm_Controller : MonoBehaviour {
        [SerializeField] private AddExpense_Button AddExpenseButton = null;
        [SerializeField] private DeleteExpense_Button DeleteExpenseButton = null;

        [SerializeField] private Currency_InputField CurrencyInputField;
        [SerializeField] private String_InputField StringInputField;
        [SerializeField] private Date_InputField DateInputField;

        private ExpenseModel Model;
        public void SetModel(ExpenseModel model) {
            Model = model;
            AddExpenseButton.Model = model;
            DeleteExpenseButton.Model = model;
        }

        public static string ConvertDecimalToString(decimal amount) => ((int)(amount * 100)).ToString();

        public static decimal ConvertStringToDecimal(string input) {
            try {
                return Convert.ToDecimal(input);
            } catch {
                return 0.00m;
            }
        }

        public static string AmountOnValueChanged(string input) {
            if (input.StartsWith("0"))
                return RemoveLeadingZero(input);
            else
                return ConvertIntoCurrencyString(input);
        }

        private static string RemoveLeadingZero(string input) => input.TrimStart('0');
        private static string ConvertIntoCurrencyString(string input) {
            input = input.PadLeft(3, '0');
            input = input.Insert(input.Length - 2, ".");
            if (input.Length > 6)
                input = input.Insert(input.Length - 6, ",");
            return input;
        }
    }
}

