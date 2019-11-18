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
        [SerializeField] private Button_Void ConfirmButton = null;

        private DateForm_HumbleView HumbleView = new DateForm_HumbleView();

        public void Awake() {
            HumbleView.Awake(Original, TileRect, MonthTitle);

            ConfirmButton.SetAction(Controller.Instance.SaveTempDayToTempExpense);
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

        public void Awake(DateElement original, RectTransform tileRect, TextMeshProUGUI monthTitle) {
            DateElements[0] = original;
            TileRect = tileRect;
            StartingTileHeight = TileRect.sizeDelta.y;

            MonthTitle = monthTitle;
        }

        public void ConstructView(DateForm_ModelCollection modelCollection) {
            ResetOriginalElement(DateElements[0], modelCollection.numOfDates);

            for (int dateIndex = 1; dateIndex < modelCollection.numOfDates; dateIndex++)
                DateElements[dateIndex] = ConstructDateElement(dateIndex);
            TileRect.sizeDelta = new Vector2(TileRect.sizeDelta.x, StartingTileHeight + RowOffset);

            for (int dateIndex = 0; dateIndex < modelCollection.numOfDates; dateIndex++) {
                if (dateIndex + 1 == modelCollection.TempDay)
                    DateElements[dateIndex].SetSelected(true);
                else
                    DateElements[dateIndex].SetSelected(false);
            }       
            MonthTitle.text = $"[{Managers.Data.Runtime.SelectedTime.Month + 0}]";
        }

        private void ResetOriginalElement(DateElement original, int arrayLength) {
            DateElements = new DateElement[arrayLength];
            DateElements[0] = original;
            DateElements[0].UpdateView(1);
        }

        private DateElement ConstructDateElement(int dateCount) {
            DateElement newDateElement = GameObject.Instantiate(original: DateElements[dateCount-1], parent: DateElements[dateCount-1].transform.parent.transform) as DateElement;
            RectTransform newRect = newDateElement.GetComponent<RectTransform>();
            if (newRect.anchoredPosition.x + DateElementOffset <= TileRect.rect.width - (TileRect.offsetMin.x + -TileRect.offsetMax.x))
                newRect.anchoredPosition = new Vector3(newRect.anchoredPosition.x + DateElementOffset, newRect.anchoredPosition.y);
            else {
                RectTransform originalRect = DateElements[0].GetComponent<RectTransform>();
                newRect.anchoredPosition = new Vector3(originalRect.anchoredPosition.x, newRect.anchoredPosition.y - DateElementOffset);
                RowOffset += DateElementOffset;
            }
            newDateElement.UpdateView(dateCount + 1);
            return newDateElement;
        }

        public void RefreshView(DateForm_ModelCollection modelCollection) {
            DeconstructView();
            ConstructView(modelCollection);
        }

        public void DeconstructView() {
            for (int Index = 1; Index < DateElements.Length; Index++)
                GameObject.Destroy(DateElements[Index].gameObject);
            RowOffset = 0f;
        }
    }

    public class DateForm_ModelCollection {
        public int numOfDates = DateTime.DaysInMonth(Managers.Data.Runtime.SelectedTime.Year, Managers.Data.Runtime.SelectedTime.Month);
        public int TempDay = Managers.Data.Runtime.TempDay;
    }
}
