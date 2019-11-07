using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class EditGoal_View : MonoBehaviour, IView {
        [SerializeField] private Currency_InputField CurrencyInput = null;
        [SerializeField] private TextMeshProUGUI AmountTitle = null;
        [SerializeField] private TextMeshProUGUI WindowTitle = null;
        [SerializeField] private Button Confirm = null;
        [SerializeField] private Button Close = null;

        private EditGoal_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new EditGoal_HumbleView();
            HumbleView.Awake(WindowTitle, AmountTitle, CurrencyInput);

            EditGoal_Controller Controller = new EditGoal_Controller();
            Close.SetController(Controller);
            Confirm.SetController(Controller);
            CurrencyInput.SetController(Controller);

            Close.SetCommandID(0);
            Confirm.SetCommandID(1);
            CurrencyInput.SetCommandID(2);
        }

        public void Activate() {
            HumbleView.ConstructView(new EditGoal_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("EditGoal_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new EditGoal_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("EditGoal_View Deactivated.");
        }
    }

    public class EditGoal_HumbleView {
        private Currency_InputField CurrencyInput = null;
        private TextMeshProUGUI AmountTitle = null;
        private TextMeshProUGUI WindowTitle = null;

        public void Awake(TextMeshProUGUI windowTitle, TextMeshProUGUI amountTitle, Currency_InputField currencyInput) {
            WindowTitle = windowTitle;
            AmountTitle = amountTitle;
            CurrencyInput = currencyInput;
        }

        public void ConstructView(EditGoal_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(EditGoal_ModelCollection modelCollection) {
            WindowTitle.text = modelCollection.Strings[25];
            AmountTitle.text = modelCollection.Strings[22];

            CurrencyInput.SetDisplayText(modelCollection.GoalModel.Amount.ToString());
        }

        public void DeconstructView() {

        }
    }

    public class EditGoal_Controller : IController {
        public void TriggerCommand(int commandID, string input = null) {
            switch (commandID) {
                case 0: Close(); break;
                case 1: SaveGoal(); break;
                case 2: SetAmount(input); break;
                default: Debug.Log("[WARNING][EditGoal_Controller] CommandID not recognized! "); return;
            }
        }

        private void Close() {
            Managers.Data.Runtime.TempGoalModel = null;
            Managers.UI.Pop();
        }

        private void SaveGoal() {
            Managers.Data.Runtime.TempGoalModel.Save();
            Managers.Data.Runtime.TempGoalModel = null;
            Managers.UI.Pop();
        }

        private void SetAmount(string input) {
            Managers.Data.Runtime.TempGoalModel.Amount = DataReformatter.ConvertStringToDecimal(input);
        }
    }

    public class EditGoal_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 22, 25 });
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
    }
}
