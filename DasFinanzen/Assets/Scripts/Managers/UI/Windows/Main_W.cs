
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [RequireComponent(typeof(Main_W_View))]
    [RequireComponent(typeof(ColorBar_View))]
    [RequireComponent(typeof(Goal_View))]
    [RequireComponent(typeof(CatagoryList_View))]
    [RequireComponent(typeof(DAS_View))]
    public class Main_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();
        private RectTransform LayerSortObject = null;

        public void Awake() {
            LayerSortObject = gameObject.transform.GetChild(0).GetComponent<RectTransform>();

            Views.Add(GetComponent<Main_W_View>());
            Views.Add(GetComponent<ColorBar_View>());
            Views.Add(GetComponent<Goal_View>());
            Views.Add(GetComponent<CatagoryList_View>());
            Views.Add(GetComponent<DAS_View>());
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

