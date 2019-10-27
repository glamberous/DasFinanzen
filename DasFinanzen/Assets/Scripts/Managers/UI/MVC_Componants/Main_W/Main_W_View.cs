using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class Main_W_View : MonoBehaviour, IView {
        [SerializeField] TextMeshProUGUI MonthText = null;
        [SerializeField] Generic_Button PreviousButton = null;
        [SerializeField] Generic_Button NextButton = null;

    private Main_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Main_W_HumbleView();

            Main_W_Controller Controller = new Main_W_Controller();
            PreviousButton.SetController(Controller);
            NextButton.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            PreviousButton.SetCommandID(0);
            NextButton.SetCommandID(1);
        }

        public void Activate() {
            HumbleView.ConstructView(new Main_W_ModelCollection(), MonthText);
            Messenger.AddListener(UIEvent.MONTH_CHANGED, Refresh);
            Debug.Log("Main_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Main_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            Messenger.RemoveListener(UIEvent.MONTH_CHANGED, Refresh);
            Debug.Log("Main_W_View Deactivated.");
        }
    }

    public class Main_W_HumbleView {
        private TextMeshProUGUI Month = null;

        public void ConstructView(Main_W_ModelCollection modelCollection, TextMeshProUGUI monthText) {
            Month = monthText;
            RefreshView(modelCollection);
        }

        public void RefreshView(Main_W_ModelCollection modelCollection) {
            Month.text = modelCollection.CurrentlySetTime.ToString("MMMM").ToUpper();
        }

        public void DeconstructView() {

        }
    }

    public class Main_W_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: DecrementMonth(); break;
                case 1: IncrementMonth(); break;
                default: Debug.Log("[WARNING][Main_W_Controller] CommandID not recognized! "); return;
            }
        }

        private void IncrementMonth() {
            DateTime newDateTime = new DateTime(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month, 1).AddMonths(1);
            Managers.Data.Runtime.SelectedTime = IfCurrentMonthReturnDateTimeNow(newDateTime);
            Messenger.Broadcast(UIEvent.MONTH_CHANGED);
        }

        private void DecrementMonth() {
            DateTime newDateTime = new DateTime(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month, 1).AddMonths(-1);
            Managers.Data.Runtime.SelectedTime = IfCurrentMonthReturnDateTimeNow(newDateTime);
            Messenger.Broadcast(UIEvent.MONTH_CHANGED);
        }

        private DateTime IfCurrentMonthReturnDateTimeNow(DateTime testDateTime) {
            if (testDateTime.Year == DateTime.Now.Year && testDateTime.Month == DateTime.Now.Month)
                return DateTime.Now;
            else
                return testDateTime;
        }
    }

    public class Main_W_ModelCollection {
        public DateTime CurrentlySetTime = Managers.Data.Runtime.SelectedTime;
    }
}
