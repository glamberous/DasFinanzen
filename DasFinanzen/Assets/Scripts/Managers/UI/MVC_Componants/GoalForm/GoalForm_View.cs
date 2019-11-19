using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class GoalForm_View : MonoBehaviour, IView {
        [SerializeField] private Currency_InputField CurrencyInput = null;
        [SerializeField] private Button_Void Confirm = null;

        private GoalForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new GoalForm_HumbleView();
            HumbleView.Awake(CurrencyInput);

            CurrencyInput.SetAction(Controller.Instance.SetTempExpenseAmount);
            Confirm.SetAction(Controller.Instance.SaveTempExpense);
        }

        public void Activate() {
            HumbleView.ConstructView(new GoalForm_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("EditGoal_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new GoalForm_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("EditGoal_View Deactivated.");
        }
    }

    public class GoalForm_HumbleView {
        private Currency_InputField CurrencyInput = null;

        public void Awake(Currency_InputField currencyInput) {
            CurrencyInput = currencyInput;
        }

        public void ConstructView(GoalForm_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(GoalForm_ModelCollection modelCollection) {
            CurrencyInput.SetDisplayText(modelCollection.GoalModel.Amount.ToString());
        }

        public void DeconstructView() {

        }
    }

    public class GoalForm_ModelCollection {
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
    }
}
