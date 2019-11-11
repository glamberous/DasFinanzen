using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Window : MonoBehaviour, IWindow {
        private IView[] Views = null;
        private GameObject LayerSorterObject = null;

        public void Awake() {
            LayerSorterObject = gameObject.transform.GetChild(0).gameObject;
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

        public void SetZLayer(float input) => LayerSorterObject.transform.localPosition = new Vector3(0, 0, input);
    }
}
