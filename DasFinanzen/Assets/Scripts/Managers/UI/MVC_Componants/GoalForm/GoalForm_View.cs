using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class GoalForm_View : MonoBehaviour, IView {
        [SerializeField] private Currency_InputField CurrencyInput = null;
        [SerializeField] private TextMeshProUGUI AmountTitle = null;
        [SerializeField] private Void_Button Confirm = null;

        private GoalForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new GoalForm_HumbleView();
            HumbleView.Awake(AmountTitle, CurrencyInput);

            GoalForm_Controller Controller = new GoalForm_Controller();
            CurrencyInput.SetController(Controller);
            Confirm.SetController(Controller);
            
            CurrencyInput.SetCommandID(0);
            Confirm.SetCommandID(1);
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
        private TextMeshProUGUI AmountTitle = null;

        public void Awake(TextMeshProUGUI amountTitle, Currency_InputField currencyInput) {
            AmountTitle = amountTitle;
            CurrencyInput = currencyInput;
        }

        public void ConstructView(GoalForm_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(GoalForm_ModelCollection modelCollection) {
            AmountTitle.text = modelCollection.Strings[22];

            CurrencyInput.SetDisplayText(modelCollection.GoalModel.Amount.ToString());
        }

        public void DeconstructView() {

        }
    }

    public class GoalForm_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 22 });
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
    }
}
