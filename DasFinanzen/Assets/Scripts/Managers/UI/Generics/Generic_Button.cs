using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Generic_Button : MonoBehaviour, IControllerElement {
        private int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        private IController Controller = null;
        public void SetController(IController controller) => Controller = controller;

        public void OnMouseDown() => Controller.TriggerCommand(CommandID);
    }
}

