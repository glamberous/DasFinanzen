using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class Main_W_View : MonoBehaviour, IView {
        [SerializeField] TextMeshProUGUI MonthText = null;
        [SerializeField] Button_Int PreviousButton = null;
        [SerializeField] Button_Int NextButton = null;

    private Main_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Main_W_HumbleView();
            HumbleView.Awake(MonthText);
        }

        public void Start() {
            PreviousButton.SetAction(Controller.Instance.AddMonthToSelectedTime, -1);
            NextButton.SetAction(Controller.Instance.AddMonthToSelectedTime, 1);
        }

        public void Activate() {
            HumbleView.ConstructView(new Main_W_ModelCollection());
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("Main_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Main_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("Main_W_View Deactivated.");
        }
    }

    public class Main_W_HumbleView {
        private TextMeshProUGUI Month = null;

        public void Awake(TextMeshProUGUI monthText) {
            Month = monthText;
        }

        public void ConstructView(Main_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Main_W_ModelCollection modelCollection) {
            // Normally Months wouldn't line up exactly with the string keys, so the + 0 is just to remind myself of that in the future.
            Month.text = modelCollection.MonthString; 
        }

        public void DeconstructView() {

        }
    }

    public class Main_W_ModelCollection {
        public DateTime CurrentlySetTime = Managers.Data.Runtime.SelectedTime;
        public string MonthString = Managers.Locale.GetString(Managers.Data.Runtime.SelectedTime.Month + 0);
    }
}
