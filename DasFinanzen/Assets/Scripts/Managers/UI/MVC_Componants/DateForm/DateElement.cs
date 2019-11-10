using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DateElement : Button {
        // Inherited from Generic_Button
        public override void OnMouseDown() => Controller.TriggerCommand(CommandID, DateTextMesh.text);

        private TextMeshProUGUI DateTextMesh = null;

        private void Awake() {
            gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void SetDate(int date) => DateTextMesh.text = date.ToString();
    }
}

