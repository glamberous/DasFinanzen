using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    [RequireComponent(typeof(TMP_InputField))]
    public class Currency_InputField : MonoBehaviour, IInputField {
        [SerializeField] private TextMeshProUGUI TextDisplay = null;
        
        private TMP_InputField TextInput = null;
        public void Awake() => TextInput = GetComponent<TMP_InputField>();

        private IController Controller = null;
        public void SetController(IController controller) => Controller = controller;

        private int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        public void OnValueChanged() {
            if (TextInput.text.StartsWith("0"))
                TextInput.text = RemoveLeadingZero(TextInput.text);
            else
                TextDisplay.text = ConvertIntStringToCurrencyString(TextInput.text);  
        }

        private string RemoveLeadingZero(string input) => input.TrimStart('0');
        private string ConvertIntStringToCurrencyString(string input) {
            input = input.PadLeft(3, '0');
            input = input.Insert(input.Length - 2, ".");
            if (input.Length > 6)
                input = input.Insert(input.Length - 6, ",");
            return input;
        }

        public void SetDisplayText(string input) => TextDisplay.text = input;
        public void OnEndEdit() => Controller.TriggerCommand(CommandID, TextDisplay.text);
    }
}

