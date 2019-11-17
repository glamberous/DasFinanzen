using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    public class Dialogue_View : MonoBehaviour, IView {
        [SerializeField] private RectTransform Tile = null;
        [SerializeField] private TextMeshProUGUI DialogueTextMesh = null;
        [SerializeField] private Button_Void CloseButton = null;

        private Dialogue_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Dialogue_HumbleView();
            HumbleView.Awake(Tile, DialogueTextMesh);

            CloseButton.SetAction(Controller.Instance.Pop);
        }

        public void Activate() {
            HumbleView.ConstructView(new Dialogue_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("Dialogue_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Dialogue_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("Dialogue_View Deactivated.");
        }
    }

    public class Dialogue_HumbleView {
        private RectTransform Tile = null;
        private TextMeshProUGUI DialogueTextMesh = null;
        private RectTransform TextMeshRect = null;

        public void Awake(RectTransform tile, TextMeshProUGUI dialogueTextMesh) {
            Tile = tile;
            DialogueTextMesh = dialogueTextMesh;
            TextMeshRect = dialogueTextMesh.GetComponent<RectTransform>();
        }

        public void ConstructView(Dialogue_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Dialogue_ModelCollection modelCollection) {
            DialogueTextMesh.text = modelCollection.Strings[modelCollection.StringKey];
            LayoutRebuilder.ForceRebuildLayoutImmediate(TextMeshRect);
            Tile.sizeDelta = new Vector2(Tile.sizeDelta.x, TextMeshRect.sizeDelta.y + 5f);
            DialogueTextMesh.transform.localPosition = new Vector3(DialogueTextMesh.transform.localPosition.x, (TextMeshRect.sizeDelta.y / 2f) * -1, DialogueTextMesh.transform.localPosition.z);
        }

        public void DeconstructView() {

        }
    }

    public class Dialogue_ModelCollection {
        public int StringKey = Managers.Data.Runtime.DialogueWindowKey;
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { Managers.Data.Runtime.DialogueWindowKey });
    }
}