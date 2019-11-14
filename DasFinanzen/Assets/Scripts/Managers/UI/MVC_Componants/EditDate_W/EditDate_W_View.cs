using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class EditDate_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button_Void CloseButton = null;

        private EditDate_W_HumbleView HumbleView = new EditDate_W_HumbleView();

        public void Awake() {
            HumbleView.Awake(TitleText);
            CloseButton.SetOnClickAction(Controller.Instance.Pop);
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
            Title.text = modelCollection.Strings[26];
        }

        public void DeconstructView() {

        }
    }

    public class EditDate_W_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 26 });
    }
}
