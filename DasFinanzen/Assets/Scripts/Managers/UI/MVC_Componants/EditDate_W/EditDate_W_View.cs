using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class EditDate_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button CloseButton = null;

        private EditDate_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new EditDate_W_HumbleView();
            HumbleView.Awake(TitleText);

            EditDate_W_Controller Controller = new EditDate_W_Controller();
            CloseButton.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            CloseButton.SetCommandID(0);
        }

        public void Activate() {
            HumbleView.ConstructView(new EditDate_W_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("EditDate_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new EditDate_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("EditDate_W_View Deactivated.");
        }
    }

    public class EditDate_W_HumbleView {
        private TextMeshProUGUI Title = null;

        public void Awake(TextMeshProUGUI title) {
            Title = title;
        }

        public void ConstructView(EditDate_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(EditDate_W_ModelCollection modelCollection) {
            Title.text = modelCollection.Strings[(Managers.Data.Runtime.SelectedTime.Month + 0)];
        }

        public void DeconstructView() {

        }
    }

    public class EditDate_W_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: Close(); break;
                default: Debug.Log("[WARNING][EditDate_W_Controller] CommandID not recognized! "); return;
            }
        }

        public void Close() => Managers.UI.Pop();
    }

    public class EditDate_W_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { (Managers.Data.Runtime.SelectedTime.Month + 0) });
    }
}
