using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_V2_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI PlannedAmount = null;
        [SerializeField] private TextMeshProUGUI SpentAmount = null;
        [SerializeField] private TextMeshProUGUI RemainingAmount = null;

        private Goal_V2_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Goal_V2_HumbleView();
            HumbleView.Awake(PlannedAmount, SpentAmount, RemainingAmount);
        }

        public void Activate() {
            HumbleView.ConstructView(new Goal_V2_ModelCollection());
            //Add any Listeners needed here.
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.GOAL_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("Goal_V2_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Goal_V2_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.GOAL_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("Goal_V2_View Deactivated.");
        }
    }

    public class Goal_V2_HumbleView {
        private TextMeshProUGUI PlannedAmount = null;
        private TextMeshProUGUI SpentAmount = null;
        private TextMeshProUGUI RemainingAmount = null;

        public void Awake(TextMeshProUGUI plannedAmount, TextMeshProUGUI spentAmount, TextMeshProUGUI remainingAmount) {
            PlannedAmount = plannedAmount;
            SpentAmount = spentAmount;
            RemainingAmount = remainingAmount;
        }

        public void ConstructView(Goal_V2_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Goal_V2_ModelCollection modelCollection) {
            decimal ExpensesTotal = DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            SpentAmount.text = "$" + ExpensesTotal.ToString();

            PlannedAmount.text = "$" + modelCollection.GoalModel.Amount.ToString();

            decimal RemainingDecimal = modelCollection.GoalModel.Amount - ExpensesTotal;
            RemainingAmount.text = "$" + RemainingDecimal.ToString();
        }

        public void DeconstructView() {

        }
    }

    public class Goal_V2_ModelCollection {
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}