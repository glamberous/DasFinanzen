using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class EditDate_W_View : MonoBehaviour, IView {
        [SerializeField] private Button_Void CloseButton = null;

        private EditDate_W_HumbleView HumbleView = new EditDate_W_HumbleView();

        public void Awake() {
            CloseButton.SetAction(Controller.Instance.Pop);
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
        public void ConstructView(EditDate_W_ModelCollection modelCollection) {

        }

        public void RefreshView(EditDate_W_ModelCollection modelCollection) {

        }

        public void DeconstructView() {

        }
    }

    public class EditDate_W_ModelCollection {
        
    }
}
