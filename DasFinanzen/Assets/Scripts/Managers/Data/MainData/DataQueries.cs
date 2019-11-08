
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace UI {
    public static class DataQueries {
        public static List<ExpenseModel> GetExpenseModels(List<ExpenseModel> expenseModels, DateTime date, int catagoryID = -1) {
            if (catagoryID == -1)
                return FilterByMonth(expenseModels, date);
            else
                return FilterByMonthAndCatagoryID(expenseModels, date, catagoryID);
        }

        private static List<ExpenseModel> FilterByMonth(List<ExpenseModel> expenseModels, DateTime date) {
            List<ExpenseModel> FilteredExpenseModels = new List<ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.Date.Month == date.Month && expenseModel.Date.Year == date.Year)
                    FilteredExpenseModels.Add(expenseModel);
            return FilteredExpenseModels;
        }

        private static List<ExpenseModel> FilterByMonthAndCatagoryID(List<ExpenseModel> expenseModels, DateTime date, int catagoryID) {
            List<ExpenseModel> FilteredExpenseModels = new List<ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.Date.Month == date.Month && expenseModel.Date.Year == date.Year && expenseModel.CatagoryID == catagoryID)
                    FilteredExpenseModels.Add(expenseModel);
            return FilteredExpenseModels;
        }

        public static CatagoryModel GetCatagoryModel(List<CatagoryModel> catagoryModels, int catagoryID) {
            foreach (CatagoryModel catagoryModel in catagoryModels)
                if (catagoryModel.CatagoryID == catagoryID)
                    return catagoryModel;
            Debug.Log($"[WARNING] Queried CatagoryModel (Key: {catagoryID}) was not found!");
            return new CatagoryModel();
        }

        public static ExpenseModel GetExpenseModel(List<ExpenseModel> expenseModels, int expenseID) {
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.ExpenseID == expenseID)
                    return expenseModel;
            Debug.Log($"[WARNING] Queried ExpenseModel (Key: {expenseID}) was not found!");
            return new ExpenseModel();
        }

        public static GoalModel GetGoalModel(List<GoalModel> goalModels, DateTime selectedTime, bool returnNullIfNotFound = false) {
            string dateKey = string.Format($"{selectedTime.Year}_{selectedTime.Month}");
            foreach (GoalModel goalModel in goalModels)
                if (goalModel.DateKey == dateKey)
                    return goalModel;
            return returnNullIfNotFound ? null : CreateNewGoalModel();
        }

        private static GoalModel CreateNewGoalModel() {
            GoalModel newGoalModel = new GoalModel();
            newGoalModel.Amount = Managers.Data.FileData.GoalModels.Count > 0 ? Managers.Data.FileData.GoalModels.Last().Amount : 1000.00m;
            newGoalModel.DateKey = string.Format($"{Managers.Data.Runtime.SelectedTime.Year}_{Managers.Data.Runtime.SelectedTime.Month}");
            newGoalModel.Save();
            Debug.Log($"New GoalModel with Key {newGoalModel.DateKey} was saved.");
            return newGoalModel;
        }
    }
}