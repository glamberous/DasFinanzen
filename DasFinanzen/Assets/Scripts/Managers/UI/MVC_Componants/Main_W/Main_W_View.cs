using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class Main_W_View : MonoBehaviour, IView {
        [SerializeField] TextMeshProUGUI MonthText = null;
        [SerializeField] TextMeshProUGUI PreviousText = null;
        [SerializeField] Button PreviousButton = null;
        [SerializeField] TextMeshProUGUI NextText = null;
        [SerializeField] Button NextButton = null;

    private Main_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Main_W_HumbleView();
            HumbleView.Awake(MonthText, PreviousText, NextText);

            Main_W_Controller Controller = new Main_W_Controller();
            PreviousButton.SetController(Controller);
            NextButton.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            PreviousButton.SetCommandID(0);
            NextButton.SetCommandID(1);
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
        private TextMeshProUGUI Prev = null;
        private TextMeshProUGUI Next = null;

        public void Awake(TextMeshProUGUI monthText, TextMeshProUGUI prevText, TextMeshProUGUI nextText) {
            Month = monthText;
            Prev = prevText;
            Next = nextText;
        }

        public void ConstructView(Main_W_ModelCollection modelCollection) {
            RefreshView(modelCollection);
        }

        public void RefreshView(Main_W_ModelCollection modelCollection) {
            // Normally Months wouldn't line up exactly with the string keys, so the + 0 is just to remind myself of that in the future.
            Month.text = modelCollection.Strings[modelCollection.CurrentlySetTime.Month + 0]; 

            Prev.text = modelCollection.Strings[13];
            Next.text = modelCollection.Strings[14];
        }

        public void DeconstructView() {

        }
    }

    public class Main_W_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: AddMonth(-1); break;
                case 1: AddMonth(1); break;
                default: Debug.Log($"[WARNING][Main_W_Controller] CommandID {commandID} not recognized! "); return;
            }
        }

        private void AddMonth(int num) {
            DateTime newDateTime = new DateTime(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month, 1).AddMonths(num);
            Managers.Data.Runtime.SelectedTime = IfCurrentMonthReturnDateTimeNow(newDateTime);
            Messenger.Broadcast(Events.MONTH_CHANGED);
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
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 } );
    }
}
