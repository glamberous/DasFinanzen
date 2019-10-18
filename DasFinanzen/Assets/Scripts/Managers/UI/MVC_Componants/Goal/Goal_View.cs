using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_View : MonoBehaviour, IView {
        [SerializeField] private RemainingElement RemainingObject = null;

        private Goal_HumbleView HumbleView = null;
        private Goal_Controller Controller = null;

        public void Awake() {
            HumbleView = new Goal_HumbleView();
            Controller = new Goal_Controller();
        }

        public void Activate() {
            HumbleView.ConstructView(Managers.Data.UIModelCollector.GetRemaining(), RemainingObject);
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
        }

        public void Deactivate() {

        }

        public void Refresh() {

        }
    }

    public class Goal_HumbleView {

        public void ConstructView(Goal_ModelCollection modelCollection, RemainingElement remainingElement) {

        }
    }

    public class Goal_Controller : IController {
        public void Close() => Managers.UI.Pop();
    }

    public class Goal_ModelCollection {
        GoalModel GoalModel = new GoalModel();
        List<ExpenseModel> CurrentMonthExpenses = new List<ExpenseModel>();
    }
}

