﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    //[RequireComponent(typeof(Catagory_W_View))]
    [RequireComponent(typeof(CatagoryList_View))]
    public class Catagory_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();

        public void Awake() {
            //Views.Add(GetComponent<Catagory_W_View>());
            Views.Add(GetComponent<CatagoryList_View>());
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
    }
}