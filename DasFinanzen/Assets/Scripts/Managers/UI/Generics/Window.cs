using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Window : MonoBehaviour, IWindow {
        private IView[] Views = null;
        private RectTransform LayerSortObject = null;

        public void Awake() {
            LayerSortObject = gameObject.transform.GetChild(0).GetComponent<RectTransform>();
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

        public void SetZLayer(float input) => LayerSortObject.localPosition = new Vector3(LayerSortObject.localPosition.x, LayerSortObject.localPosition.y, input);
    }

}
