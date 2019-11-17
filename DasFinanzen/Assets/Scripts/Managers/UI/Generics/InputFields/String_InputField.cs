using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    [RequireComponent(typeof(TMP_InputField))]
    public class String_InputField : GenericElement<string>, IInputField<string> {
        private TMP_InputField TextInput = null;
        public void Awake() => TextInput = GetComponent<TMP_InputField>();

        public void SetDisplayText(string input) => TextInput.text = input;
        public void OnEndEdit() => Action(TextInput.text);
    }
}
