using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    [RequireComponent(typeof(TMP_InputField))]
    public class String_InputField : MonoBehaviour, IControllerElement {
        private TMP_InputField TextInput = null;
        public void Awake() => TextInput = GetComponent<TMP_InputField>();

        private IController Controller = null;
        public void SetController(IController controller) => Controller = controller;

        private int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        public void OnEndEdit() => Controller.TriggerCommand(CommandID, TextInput.text);
    }
}
