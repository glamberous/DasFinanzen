using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI RemainingText = null;
        [SerializeField] private Generic_Button GoalWindowTrigger = null;

        private Goal_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Goal_HumbleView();

            Goal_Controller Controller = new Goal_Controller();
            GoalWindowTrigger.SetController(Controller);
            GoalWindowTrigger.SetCommandID(0);
        }

        public void Activate() {
            HumbleView.ConstructView(new Goal_ModelCollection(), RemainingText);
            Messenger.AddListener(UIEvent.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(UIEvent.GOAL_UPDATED, Refresh);
        }

        public void Refresh() {
            HumbleView.RefreshView(new Goal_ModelCollection());
        }

        public void Deactivate() {
            Messenger.RemoveListener(UIEvent.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(UIEvent.GOAL_UPDATED, Refresh);
            HumbleView.DeconstructView();
        }
    }

    public class Goal_HumbleView {
        private TextMeshProUGUI Remaining = null;

        public void ConstructView(Goal_ModelCollection modelCollection, TextMeshProUGUI remainingText) {
            Remaining = remainingText;
            RefreshView(modelCollection);
        }

        public void RefreshView(Goal_ModelCollection modelCollection) {
            decimal RemainingAmount = modelCollection.GoalModel.Amount - DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            Remaining.text = RemainingAmount.ToString();
        }

        public void DeconstructView() {

        }
    }

    public class Goal_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch(commandID) {
                case 0: TriggerGoalWindow(); break;
                default: Debug.Log("[WARNING][Goal_Controller] CommandID not recognized! "); return;
            }
        }

        private void TriggerGoalWindow() => Managers.UI.Push(WINDOW.GOAL);
    }

    public class Goal_ModelCollection {
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}

