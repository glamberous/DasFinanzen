using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Expense_W : MonoBehaviour, IWindow {
        private List<IView> Views = new List<IView>();

        public void Awake() {
            //Views.Add(GetComponent<ExpenseDataEntry_View>());
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

