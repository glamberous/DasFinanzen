using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class ExpenseModelForm_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI AmountText = null;
        [SerializeField] private TextMeshProUGUI NameText = null;
        [SerializeField] private TextMeshProUGUI DateText = null;

        [HideInInspector] public ExpenseModel Model { get; private set; } = null;

        public void Activate() {
            Refresh();
            Debug.Log("ExpenseDataEntryView Activated.");
        }

        public void Refresh() {
            Model = Managers.Data.Runtime.CurrentExpenseID == -1 ? new ExpenseModel() : DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.CurrentExpenseID);
            AmountText.text = Model.Amount.ToString();
            NameText.text = Model.NameText.ToString();
            DateText.text = Model.Date.ToString("mm/dd");
        }

        public void Deactivate() => Debug.Log("ExpenseDataEntryView Deactivated.");
    }

    public static class ExpenseModelForm_Controller {
        public static void SaveExpense(ExpenseModel Model) {
            Model.Save();
            Managers.UI.Pop();
        }

        public static void DeleteExpense(ExpenseModel Model) {
            Model.Delete();
            Managers.UI.Pop();
        }
    }
}

