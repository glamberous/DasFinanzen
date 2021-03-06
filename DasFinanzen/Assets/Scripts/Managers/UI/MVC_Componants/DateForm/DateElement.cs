﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace UI {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DateElement : Button_Int {
        // Inherited from Generic_Button
        //public override void OnMouseUp() => Controller.TriggerCommand(CommandID, DateTextMesh.text);

        private TextMeshProUGUI DateTextMesh = null;
        private GameObject SelectionBox = null;

        public void SetSelected(bool selected) => SelectionBox.SetActive(selected);

        private void Awake() {
            DateTextMesh = gameObject.GetComponent<TextMeshProUGUI>();
            SelectionBox = gameObject.transform.GetChild(0).gameObject;
        }

        public void UpdateView(int date) {
            DateTextMesh.text = date.ToString();
            SetAction(Controller.Instance.SetTempDay, date);
        }
    }
}

