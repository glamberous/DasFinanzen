using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_V2_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI PlannedText = null;
        [SerializeField] private TextMeshProUGUI PlannedAmount = null;
        [SerializeField] private TextMeshProUGUI SpentText = null;
        [SerializeField] private TextMeshProUGUI SpentAmount = null;
        [SerializeField] private TextMeshProUGUI RemainingText = null;
        [SerializeField] private TextMeshProUGUI RemainingAmount = null;

        private Goal_V2_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Goal_V2_HumbleView();
            HumbleView.Awake(PlannedText, PlannedAmount, SpentText, SpentAmount, RemainingText, RemainingAmount);

            //Goal_V2_Controller Controller = new Goal_V2_Controller();
            //Example.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            //Example.SetCommandID(0); 
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
        private TextMeshProUGUI PlannedText = null;
        private TextMeshProUGUI PlannedAmount = null;
        private TextMeshProUGUI SpentText = null;
        private TextMeshProUGUI SpentAmount = null;
        private TextMeshProUGUI RemainingText = null;
        private TextMeshProUGUI RemainingAmount = null;

        public void Awake(TextMeshProUGUI plannedText, TextMeshProUGUI plannedAmount, TextMeshProUGUI spentText, TextMeshProUGUI spentAmount, TextMeshProUGUI remainingText, TextMeshProUGUI remainingAmount) {
            PlannedText = plannedText;
            PlannedAmount = plannedAmount;
            SpentText = spentText;
            SpentAmount = spentAmount;
            RemainingText = remainingText;
            RemainingAmount = remainingAmount;
        }

        public void ConstructView(Goal_V2_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Goal_V2_ModelCollection modelCollection) {
            RemainingText.text = modelCollection.Strings[20];
            SpentText.text = modelCollection.Strings[18];
            PlannedText.text = modelCollection.Strings[19];

            decimal ExpensesTotal = DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            SpentAmount.text = "$" + ExpensesTotal.ToString();

            PlannedAmount.text = "$" + modelCollection.GoalModel.Amount.ToString();

            decimal RemainingDecimal = modelCollection.GoalModel.Amount - ExpensesTotal;
            RemainingAmount.text = "$" + RemainingDecimal.ToString();
        }

        public void DeconstructView() {

        }
    }

    public class Goal_V2_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: break;
                default: Debug.Log("[WARNING][Goal_V2_Controller] CommandID not recognized! "); return;
            }
        }
    }

    public class Goal_V2_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 18, 19, 20 });
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}