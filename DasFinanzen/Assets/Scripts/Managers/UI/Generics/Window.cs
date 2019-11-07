using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Window : MonoBehaviour, IWindow {
        private IView[] Views = null;
        private Canvas Canvas = null;

        public void Awake() {
            Canvas = gameObject.GetComponent<Canvas>();
            Views = GetComponentsInChildren<IView>();
        }

        public IWindow Activate() {
            // You need to set it active before Activating/Initializing all the views otherwise Awake() doesn't get called.
            gameObject.SetActive(true);

            foreach (IView view in Views)
                view.Activate();

            return this;
        }

        public void Deactivate() {
            gameObject.SetActive(false);

            foreach (IView view in Views)
                view.Deactivate();
        }

        public void SetLayer(int input) => Canvas.sortingOrder = input;
    }
}
