using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class EditGoal_W_View : MonoBehaviour, IView {
        [SerializeField] private Button_Void CloseButton = null;

        private EditGoal_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new EditGoal_W_HumbleView();
            CloseButton.SetAction(Controller.Instance.Pop);
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
        public void Awake(TextMeshProUGUI title) {

        }

        public void ConstructView(EditGoal_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(EditGoal_W_ModelCollection modelCollection) {

        }

        public void DeconstructView() {

        }
    }

    public class EditGoal_W_ModelCollection {
        
    }
}
