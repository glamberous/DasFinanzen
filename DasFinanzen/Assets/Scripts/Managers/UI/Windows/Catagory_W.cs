
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [RequireComponent(typeof(Catagory_W_View))]
    [RequireComponent(typeof(ExpenseList_View))]
    public class Catagory_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();
        private RectTransform LayerSortObject = null;

        public void Awake() {
            LayerSortObject = gameObject.transform.GetChild(0).GetComponent<RectTransform>();

            Views.Add(GetComponent<Catagory_W_View>());
            Views.Add(GetComponent<ExpenseList_View>());
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