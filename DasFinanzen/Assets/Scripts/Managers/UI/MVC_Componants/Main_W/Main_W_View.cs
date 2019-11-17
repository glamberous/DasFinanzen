using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class Main_W_View : MonoBehaviour, IView {
        [SerializeField] TextMeshProUGUI MonthText = null;
        [SerializeField] TextMeshProUGUI PreviousText = null;
        [SerializeField] Button_Int PreviousButton = null;
        [SerializeField] TextMeshProUGUI NextText = null;
        [SerializeField] Button_Int NextButton = null;

    private Main_W_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new Main_W_HumbleView();
            HumbleView.Awake(MonthText, PreviousText, NextText);
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

    public class Main_W_ModelCollection {
        public DateTime CurrentlySetTime = Managers.Data.Runtime.SelectedTime;
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 } );
    }
}
