using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Remaining_View : MonoBehaviour, IView {
        [SerializeField] private RemainingElement RemainingObject = null;

        private Remaining_HumbleView HumbleView = null;
        private Remaining_Controller Controller = null;

        public void Awake() {
            HumbleView = new Remaining_HumbleView();
            Controller = new Remaining_Controller();
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

    public class Remaining_HumbleView {

        public void ConstructView(Remaining_ModelCollection modelCollection, RemainingElement )
    }

    public class Remaining_Controller : IController {
        public void Close() => Managers.UI.Pop();
    }

    public class Remaining_ModelCollection {
        decimal Goal = 1000.00m;
        List<ExpenseModel> ExpensesInCurrentMonth = new List<ExpenseModel>();
    }
}

