using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class Button : MonoBehaviour, IControllerElement {
        public virtual void OnMouseUp() => Controller.TriggerCommand(CommandID);

        protected int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        protected IController Controller = null;
        public void SetController(IController controller) => Controller = controller;
    }
}

