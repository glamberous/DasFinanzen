using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    public sealed class Controller {
        static Controller() { }
        private Controller() { }
        public static Controller Instance { get; } = new Controller();

        private RuntimeData Runtime = Managers.Data.Runtime;
        private FileData FileData = Managers.Data.FileData;
        private UIManagerHumble Manager = Managers.UI;

        #region Generic
        public void Pop(Void input = null) => Managers.UI.Pop();
        #endregion

        #region PushUI
        public void PushEditDateWindow(int input) {
            Runtime.TempDay = Runtime.TempExpenseModel.Date.Day;
            Manager.Push(WINDOW.EDIT_DATE);
        }

        public void PushEditExpenseWindow(int input) {
            Runtime.TempExpenseModel = DataQueries.GetExpenseModel(FileData.ExpenseModels, input);
            Manager.Push(WINDOW.EXPENSE);
        }

        public void PushAddExpenseWindow(Void input = null) {
            Runtime.TempExpenseModel = new ExpenseModel();
            Manager.Push(WINDOW.EXPENSE);
        }

        public void PushCatagoryWindow(int input) {
            Runtime.CurrentCatagoryID = input;
            Manager.Push(WINDOW.CATAGORY);
        }

        public void PushGoalWindow(Void input = null) {
            Runtime.TempGoalModel = DataQueries.GetGoalModel(FileData.GoalModels, Runtime.SelectedTime);
            Manager.Push(WINDOW.EDIT_GOAL);
        }

        public void PushDialogueWindow(int input) {
            Runtime.DialogueWindowKey = input;
            Manager.Push(WINDOW.DIALOGUE);
        }
        #endregion

        #region TempExpense
        public void SaveTempExpense(Void input = null) {
            Runtime.TempExpenseModel.Save();
            Runtime.TempExpenseModel = null;
            Manager.Pop();
        }

        public void DeleteTempExpense(Void input = null) {
            Runtime.TempExpenseModel.Delete();
            Runtime.TempExpenseModel = null;
            Manager.Pop();
        }

        public void SetTempExpenseAmount(decimal input) {
            Runtime.TempExpenseModel.Amount = input;
        }

        public void SetTempExpenseName(string input) {
            Runtime.TempExpenseModel.NameText = input;
        }

        public void SetTempDay(int input) {
            Runtime.TempDay = input;
            Messenger.Broadcast(Events.TEMP_DAY_UPDATED);
        }

        public void SaveTempDayToTempExpense(Void input = null) {
            Runtime.TempExpenseModel.Date = Runtime.TempExpenseModel.Date.AddDays(Runtime.TempDay - Runtime.TempExpenseModel.Date.Day);
            Messenger.Broadcast(Events.TEMP_EXPENSE_UPDATED);
        }
        #endregion

        #region TempGoal
        public void SaveTempGoal(Void input = null) {
            Runtime.TempGoalModel.Save();
            Runtime.TempGoalModel = null;
        }

        public void SetTempGoalAmount(decimal input) {
            Runtime.TempGoalModel.Amount = input;
        }
        #endregion

        #region SelectedTime
        public void AddMonthToSelectedTime(int input) {
            DateTime newDateTime = new DateTime(Runtime.SelectedTime.Year, Runtime.SelectedTime.Month, 1).AddMonths(input);
            Runtime.SelectedTime = IfCurrentMonthReturnDateTimeNow(newDateTime);
            Messenger.Broadcast(Events.MONTH_CHANGED);
        }

        private static DateTime IfCurrentMonthReturnDateTimeNow(DateTime testDateTime) {
            if (testDateTime.Year == DateTime.Now.Year && testDateTime.Month == DateTime.Now.Month)
                return DateTime.Now;
            else
                return testDateTime;
        }
        #endregion
    }
}

