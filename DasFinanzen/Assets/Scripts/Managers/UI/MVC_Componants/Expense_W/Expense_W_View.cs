using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Expense_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button_Void BackButton = null;

        private Expense_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Expense_W_HumbleView();
            HumbleView.Awake(TitleText);

            BackButton.SetAction(Controller.Instance.Pop);
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
            PageTitle.text = modelCollection.TitleString;
        }

        public void DeconstructView() {

        }
    }

    public class Expense_W_ModelCollection {
        public ExpenseModel ExpenseModel = Managers.Data.Runtime.TempExpenseModel;
        public string TitleString = IDTracker.IsNew(Managers.Data.Runtime.TempExpenseModel.ExpenseID) ? Managers.Locale.GetString(23) : Managers.Locale.GetString(21);
    }
}

