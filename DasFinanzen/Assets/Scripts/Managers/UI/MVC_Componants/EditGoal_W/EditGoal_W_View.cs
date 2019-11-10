using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class EditGoal_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button CloseButton = null;

        private EditGoal_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new EditGoal_W_HumbleView();
            HumbleView.Awake(TitleText);

            EditGoal_W_Controller Controller = new EditGoal_W_Controller();
            CloseButton.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            CloseButton.SetCommandID(0); 
        }

        public void Activate() {
            HumbleView.ConstructView(new EditGoal_W_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("EditGoal_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new EditGoal_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("EditGoal_W_View Deactivated.");
        }
    }

    public class EditGoal_W_HumbleView {
        private TextMeshProUGUI Title = null;

        public void Awake(TextMeshProUGUI title) {
            Title = title;
        }

        public void ConstructView(EditGoal_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(EditGoal_W_ModelCollection modelCollection) {
            Title.text = modelCollection.Strings[25];
        }

        public void DeconstructView() {

        }
    }

    public class EditGoal_W_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: Close(); break;
                default: Debug.Log("[WARNING][EditGoal_W_Controller] CommandID not recognized! "); return;
            }
        }

        public void Close() => Managers.UI.Pop();
    }

    public class EditGoal_W_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 25 });
    }
}
