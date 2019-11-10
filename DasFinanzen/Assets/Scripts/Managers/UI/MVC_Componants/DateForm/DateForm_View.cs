using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    public class DateForm_View : MonoBehaviour, IView {
        [SerializeField] private DateElement Original = null;
        [SerializeField] private RectTransform CanvasRect = null;

        private DateForm_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new DateForm_HumbleView();
            HumbleView.Awake(Original, CanvasRect.sizeDelta.x);

            //DateForm_Controller Controller = new DateForm_Controller();
            //Example.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            //Example.SetCommandID(0); 
        }

        public void Activate() {
            HumbleView.ConstructView(new DateForm_ModelCollection());
            //Add any Listeners needed here.
            Debug.Log("DateForm_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new DateForm_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("DateForm_View Deactivated.");
        }
    }

    public class DateForm_HumbleView {
        private const float DateElementOffset = 30f;
        private float StartingTileHeight = 0f;
        private float ScreenWidth = 0f;
        private int RowCount = 0;
        private DateElement[] DateElements = new DateElement[31];
        private RectTransform Tile = null;

        public void Awake(DateElement original, float screenWidth) {
            DateElements[0] = original;
            ScreenWidth = screenWidth;
            Tile = original.transform.parent.gameObject.GetComponent<RectTransform>();
            StartingTileHeight = Tile.sizeDelta.y;
        }

        public void ConstructView(DateForm_ModelCollection modelCollection) {
            for (int dateCount = 1; dateCount < modelCollection.numOfDates; dateCount++) {
                DateElements[dateCount] = ConstructDateElement(dateCount);
            }
            Tile.sizeDelta = new Vector2(Tile.sizeDelta.x, StartingTileHeight + (RowCount * DateElementOffset));
        }

        private DateElement ConstructDateElement(int dateCount) {
            DateElement newDateElement = GameObject.Instantiate(original: DateElements[dateCount-1], parent: DateElements[dateCount-1].transform.parent.transform) as DateElement;
            newDateElement.SetDate(dateCount);
            if (newDateElement.transform.localPosition.x + DateElementOffset <= ScreenWidth)
                newDateElement.transform.localPosition = new Vector3(newDateElement.transform.localPosition.x + DateElementOffset, newDateElement.transform.localPosition.y, newDateElement.transform.localPosition.z);
            else {
                RowCount++;
                newDateElement.transform.localPosition = new Vector3(DateElements[0].transform.localPosition.x, DateElements[0].transform.localPosition.y + (DateElementOffset * RowCount), DateElements[0].transform.localPosition.z);
            }
            return newDateElement;
        }

        public void RefreshView(DateForm_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            for (int Index = 1; Index < DateElements.Length; Index++)
                GameObject.Destroy(DateElements[Index].gameObject);
            RowCount = 0;
        }
    }

    public class DateForm_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: SetDate(Convert.ToInt32(input)); break;
                default: Debug.Log("[WARNING][DateForm_Controller] CommandID not recognized! "); return;
            }
        }

        private void SetDate(int date) {

        }
    }

    public class DateForm_ModelCollection {
        public int numOfDates = DateTime.DaysInMonth(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month);
    }
}
