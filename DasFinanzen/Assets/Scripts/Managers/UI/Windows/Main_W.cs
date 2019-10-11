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
            //Views.Add(GetComponent<ColorBar_V>());
            //Views.Add(GetComponent<Remaining_V>());
        }

        public IWindow Activate() {
            foreach (IView view in Views)
                view.Activate();

            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(AppEvent.GOAL_UPDATED, Refresh);

            gameObject.SetActive(true);
            return this;
        }

        public void Deactivate() {
            gameObject.SetActive(false);

            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(AppEvent.GOAL_UPDATED, Refresh);

            foreach (IView view in Views)
                view.Deactivate();
        }

        public void Refresh() {
            foreach (IView view in Views)
                view.Refresh();
        }
    }
}

