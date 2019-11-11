using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class DateForm_View : MonoBehaviour, IView {
        [SerializeField] private DateElement Original = null;
        [SerializeField] private RectTransform TileRect = null;
        [SerializeField] private TextMeshProUGUI MonthTitle = null;
        [SerializeField] private Button ConfirmButton = null;

        private DateForm_HumbleView HumbleView = new DateForm_HumbleView();
        private DateForm_Controller Controller = null;

        public void Awake() {
            DateForm_Controller Controller = new DateForm_Controller();
            HumbleView.Awake(Controller, Original, TileRect, MonthTitle);

            //DateForm_Controller Controller = new DateForm_Controller();
            ConfirmButton.SetController(Controller);
            
            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            ConfirmButton.SetCommandID(1);
        }

        public void Activate() {
            HumbleView.ConstructView(new DateForm_ModelCollection());
            //Add any Listeners needed here.
            Messenger.AddListener(Events.TEMP_DAY_UPDATED, Refresh);
            Debug.Log("DateForm_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new DateForm_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Messenger.RemoveListener(Events.TEMP_DAY_UPDATED, Refresh);
            Debug.Log("DateForm_View Deactivated.");
        }
    }

    public class DateForm_HumbleView {
        private const float DateElementOffset = 30f;
        private float StartingTileHeight = 0f;
        private float RowOffset = 0f;
        private DateElement[] DateElements = new DateElement[1];
        private RectTransform TileRect = null;
        private TextMeshProUGUI MonthTitle = null;
        private DateForm_Controller Controller = null;

        public void Awake(DateForm_Controller controller, DateElement original, RectTransform parentRect, TextMeshProUGUI monthTitle) {
            Controller = controller;
            DateElements[0] = original;
            TileRect = parentRect;
            StartingTileHeight = TileRect.sizeDelta.y;
            MonthTitle = monthTitle;
        }

        public void ConstructView(DateForm_ModelCollection modelCollection) {
            DateElement TempElement = DateElements[0];
            DateElements = new DateElement[modelCollection.numOfDates];
            DateElements[0] = TempElement;
            DateElements[0].SetDate(1);
            DateElements[0].SetController(Controller);
            DateElements[0].SetCommandID(0);

            for (int dateIndex = 1; dateIndex < modelCollection.numOfDates; dateIndex++)
                DateElements[dateIndex] = ConstructDateElement(dateIndex);
            TileRect.sizeDelta = new Vector2(TileRect.sizeDelta.x, StartingTileHeight + RowOffset);

            for (int dateIndex = 0; dateIndex < modelCollection.numOfDates; dateIndex++) {
                if (dateIndex + 1 == modelCollection.TempDay)
                    DateElements[dateIndex].SetSelected(true);
                else
                    DateElements[dateIndex].SetSelected(false);
            }
            MonthTitle.text = modelCollection.Strings[Managers.Data.Runtime.SelectedTime.Month + 0];
        }

        private DateElement ConstructDateElement(int dateCount) {
            DateElement newDateElement = GameObject.Instantiate(original: DateElements[dateCount-1], parent: DateElements[dateCount-1].transform.parent.transform) as DateElement;
            newDateElement.SetDate(dateCount + 1);
            RectTransform newRect = newDateElement.GetComponent<RectTransform>();
            if (newRect.anchoredPosition.x + DateElementOffset <= TileRect.rect.width - (TileRect.offsetMin.x + -TileRect.offsetMax.x))
                newRect.anchoredPosition = new Vector3(newRect.anchoredPosition.x + DateElementOffset, newRect.anchoredPosition.y);
            else {
                RectTransform originalRect = DateElements[0].GetComponent<RectTransform>();
                newRect.anchoredPosition = new Vector3(originalRect.anchoredPosition.x, newRect.anchoredPosition.y - DateElementOffset);
                RowOffset += DateElementOffset;
            }
            newDateElement.SetController(Controller);
            newDateElement.SetCommandID(0);
            return newDateElement;
        }

        public void RefreshView(DateForm_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            for (int Index = 1; Index < DateElements.Length; Index++)
                if (DateElements[Index] != null)
                    GameObject.Destroy(DateElements[Index].gameObject);
            RowOffset = 0f;
        }
    }

    public class DateForm_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: SetDate(Convert.ToInt32(input)); break;
                case 1: SaveDate(); break;
                default: Debug.Log("[WARNING][DateForm_Controller] CommandID not recognized! "); return;
            }
        }

        private void SetDate(int date) {
            Managers.Data.Runtime.TempDay = date;
            Messenger.Broadcast(Events.TEMP_DAY_UPDATED);
        }

        private void SaveDate() {
            Managers.Data.Runtime.TempExpenseModel.Date = Managers.Data.Runtime.TempExpenseModel.Date.AddDays(Managers.Data.Runtime.TempDay - Managers.Data.Runtime.TempExpenseModel.Date.Day);
            Messenger.Broadcast(Events.TEMP_EXPENSE_UPDATED);
            Managers.UI.Pop();
        }
    }

    public class DateForm_ModelCollection {
        public Dictionary<int, string> Strings = Managers.Locale.GetStringDict(new int[] { (Managers.Data.Runtime.SelectedTime.Month + 0) });
        public int numOfDates = DateTime.DaysInMonth(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month);
        public int TempDay = Managers.Data.Runtime.TempDay;
    }
}
