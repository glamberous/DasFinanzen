using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {
    public class DAS_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI BDAS = null;
        [SerializeField] private TextMeshProUGUI ADAS = null;

        private DAS_HumbleView HumbleView = null;

        public void Awake() {
            HumbleView = new DAS_HumbleView();

            //DAS_Controller Controller = new DAS_Controller();
            //Example.SetController(Controller);

            //Cross reference the Command ID's from the Controller class near the bottom of this page.
            //Example.SetCommandID(0); 
        }

        public void Activate() {
            HumbleView.ConstructView(new DAS_ModelCollection(), BDAS, ADAS);
            //Add any Listeners needed here.
            Messenger.AddListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.AddListener(Events.GOAL_UPDATED, Refresh);
            Messenger.AddListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("DAS_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new DAS_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Messenger.RemoveListener(Events.EXPENSES_UPDATED, Refresh);
            Messenger.RemoveListener(Events.GOAL_UPDATED, Refresh);
            Messenger.RemoveListener(Events.MONTH_CHANGED, Refresh);
            Debug.Log("DAS_View Deactivated.");
        }
    }

    public class DAS_HumbleView {
        private TextMeshProUGUI BDAS = null;
        private TextMeshProUGUI ADAS = null;

        public void ConstructView(DAS_ModelCollection modelCollection, TextMeshProUGUI bdas, TextMeshProUGUI adas) {
            BDAS = bdas;
            ADAS = adas;

            RefreshView(modelCollection);
        }

        public void RefreshView(DAS_ModelCollection modelCollection) {
            BDAS.text = "$" + DailyAverageSpendCalculator.Before(modelCollection).ToString("0.00");
            if (modelCollection.CurrentlySetTime.Month < DateTime.Now.Month && modelCollection.CurrentlySetTime.Year <= DateTime.Now.Year) 
                ADAS.text = "N/A";
            else
                ADAS.text = "$" + DailyAverageSpendCalculator.After(modelCollection).ToString("0.00");
        }

        public void DeconstructView() {

        }
    }

    internal static class DailyAverageSpendCalculator {
        public static decimal Before(DAS_ModelCollection modelCollection) {
            decimal totalSpentInMonth = DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            if (modelCollection.CurrentlySetTime.Month < DateTime.Now.Month && modelCollection.CurrentlySetTime.Year <= DateTime.Now.Year)
                return totalSpentInMonth / DateTime.DaysInMonth(modelCollection.CurrentlySetTime.Year, modelCollection.CurrentlySetTime.Month);
            else
                return totalSpentInMonth / modelCollection.CurrentlySetTime.Day;
        }

        public static decimal After(DAS_ModelCollection modelCollection) {
            decimal totalSpentInMonth = DataReformatter.GetExpensesTotal(modelCollection.ExpenseModels);
            decimal amountLeftToSpend = modelCollection.GoalModel.Amount - totalSpentInMonth;

            DateTime firstOfNextMonth = new DateTime(modelCollection.CurrentlySetTime.Year, modelCollection.CurrentlySetTime.Month, 1).AddMonths(1);
            int daysLeftInMonth = (firstOfNextMonth - modelCollection.CurrentlySetTime).Days;

            return amountLeftToSpend / (daysLeftInMonth + 1);
        }
    }

    public class DAS_Controller : IController {
        public void TriggerCommand(int commandID, string input) {
            switch (commandID) {
                case 0: break;
                default: Debug.Log("[WARNING][DAS_Controller] CommandID not recognized! "); return;
            }
        }
    }

    public class DAS_ModelCollection {
        // Put Model Collections Here
        public DateTime CurrentlySetTime = Managers.Data.Runtime.SelectedTime;
        public GoalModel GoalModel = DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        public List<ExpenseModel> ExpenseModels = DataQueries.GetExpenseModels(Managers.Data.FileData.ExpenseModels, Managers.Data.Runtime.SelectedTime);
    }
}