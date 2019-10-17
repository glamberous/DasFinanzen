using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    [RequireComponent(typeof(Catagory_V))]
    [RequireComponent(typeof(ColorBar_V))]
    [RequireComponent(typeof(Remaining_V))]
    public class Main_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();

        public void Awake() {
            Views.Add(GetComponent<Catagory_V>());
            Views.Add(GetComponent<ColorBar_V>());
            //Views.Add(GetComponent<Remaining_V>());
        }

        public IWindow Activate() {
            foreach (IView view in Views)
                view.Activate();

            gameObject.SetActive(true);
            return this;
        }

        public void Deactivate() {
            gameObject.SetActive(false);

            foreach (IView view in Views)
                view.Deactivate();
        }
    }
}

