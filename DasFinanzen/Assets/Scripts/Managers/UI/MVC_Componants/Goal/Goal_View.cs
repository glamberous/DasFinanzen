using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_View : MonoBehaviour, IView {
        [SerializeField] private GoalElement GoalObject = null;

        private Goal_HumbleView HumbleView = null;
        private Goal_Controller Controller = null;

        public void Awake() {
            HumbleView = new Goal_HumbleView();
        }

        public void Activate() {
            HumbleView.ConstructView(new Goal_ModelCollection(), GoalObject);
            Messenger.AddListener(UIEvent.EXPENSES_UPDATED, Refresh);
        }

        public void Refresh() {
            HumbleView.RefreshView(new Goal_ModelCollection());
        }

        public void Deactivate() {
            Messenger.RemoveListener(UIEvent.EXPENSES_UPDATED, Refresh);
            HumbleView.DeconstructView();
        }
    }

    public class Goal_HumbleView {
        public void ConstructView(Goal_ModelCollection modelCollection, GoalElement goalElement) {

        }

        public void RefreshView(Goal_ModelCollection modelCollection) {

        }

        public void DeconstructView() {

        }
    }

    public class Goal_Controller : IController {
        public void TriggerCommand(int commandID, string input) {

        }
    }

    public class Goal_ModelCollection {
        public GoalModel GoalModel = Managers.Data.FileData.GoalModel;
        public List<ExpenseModel> ExpenseModels = DataReformatter.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}

