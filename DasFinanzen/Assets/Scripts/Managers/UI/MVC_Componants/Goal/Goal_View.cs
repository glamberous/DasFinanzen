using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Goal_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI AmountText = null;
        [SerializeField] private TextMeshProUGUI RemainingText = null;
        [SerializeField] private Button_Void GoalWindowButton = null;

        private Goal_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Goal_HumbleView();
            HumbleView.Awake(AmountText, RemainingText);
        }

        public void Start() {
            GoalWindowButton.SetAction(Controller.Instance.PushGoalWindow);
        }

        public void Activate() {
            HumbleView.ConstructView(new Goal_ModelCollection());
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.GOAL_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
        }

        public void Refresh() {
            HumbleView.RefreshView(new Goal_ModelCollection());
        }

        public void Deactivate() {
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.GOAL_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            HumbleView.DeconstructView();
        }
    }

    public class Goal_HumbleView {
        private TextMeshProUGUI Amount = null;
        private TextMeshProUGUI Remaining = null;

        public void Awake(TextMeshProUGUI amountText, TextMeshProUGUI remainingText) {
            Amount = amountText;
            Remaining = remainingText;
        }

        public void ConstructView(Goal_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Goal_ModelCollection modelCollection) {
            Remaining.text = modelCollection.Strings[15];

            decimal RemainingAmount = modelCollection.GoalModel.Amount - DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            Amount.text = "$" + RemainingAmount.ToString();
        }

        public void DeconstructView() {

        }
    }

    public class Goal_ModelCollection {
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 15 });
    }
}

