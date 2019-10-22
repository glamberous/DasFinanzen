using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    //[RequireComponent(typeof(Header_View))]
    [RequireComponent(typeof(ColorBar_View))]
    //[RequireComponent(typeof(Goal_View))]
    [RequireComponent(typeof(Catagory_View))]
    //[RequireComponent(typeof(Footer_View))]
    public class Main_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();

        public void Awake() {
            //Views.Add(GetComponent<Header_View());
            Views.Add(GetComponent<ColorBar_View>());
            //Views.Add(GetComponent<Goal_View>());
            Views.Add(GetComponent<Catagory_View>());
            //Views.Add(GetComponent<Footer_View());
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

