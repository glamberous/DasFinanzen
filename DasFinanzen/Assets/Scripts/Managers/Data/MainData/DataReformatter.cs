﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace UI {
    public static class DataReformatter {
        public static Dictionary<int, decimal> GetExpenseTotalsDict(List<CatagoryModel> catagoryModels, List<ExpenseModel> expenseModels) {
            Dictionary<int, decimal> ExpenseTotals = new Dictionary<int, decimal>();
            foreach (CatagoryModel catagoryModel in catagoryModels)
                ExpenseTotals[catagoryModel.CatagoryID] = 0.00m;
            foreach (ExpenseModel expenseModel in expenseModels)
                ExpenseTotals[expenseModel.CatagoryID] += expenseModel.Amount;
            return ExpenseTotals;
        }

        public static Dictionary<int, CatagoryModel> GetCatagoryModelsDict(List<CatagoryModel> catagoryModels) {
            Dictionary<int, CatagoryModel> CatagoryModelDict = new Dictionary<int, CatagoryModel>();
            foreach (CatagoryModel catagoryModel in catagoryModels)
                CatagoryModelDict[catagoryModel.CatagoryID] = catagoryModel;
            return CatagoryModelDict;
        }

        public static decimal GetExpensesTotal(List<ExpenseModel> expenseModels) {
            decimal Total = 0.00m;
            foreach (ExpenseModel expense in expenseModels)
                Total += expense.Amount;
            return Total;
        }

        public static CatagoryModel GetCatagoryModel(List<CatagoryModel> catagoryModels, int catagoryID) {
            foreach (CatagoryModel catagoryModel in catagoryModels)
                if (catagoryModel.CatagoryID == catagoryID)
                    return catagoryModel;
            return null;
        }

        public static ExpenseModel GetExpenseModel(List<ExpenseModel> expenseModels, int expenseID) {
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.ExpenseID == expenseID)
                    return expenseModel;
            return null;
        }

        public static List<ExpenseModel> GetExpenseModels(List<ExpenseModel> expenseModels, DateTime date, int catagoryID = -1) {
            if (catagoryID == -1)
                return FilterByMonth(expenseModels, date);
            else
                return FilterByMonthAndCatagoryID(expenseModels, date, catagoryID);
        }

        private static List<ExpenseModel> FilterByMonth(List<ExpenseModel> expenseModels, DateTime date) {
            List<ExpenseModel> newExpenseModelList = new List<ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.Date.Month == date.Month && expenseModel.Date.Year == date.Year)
                    newExpenseModelList.Add(expenseModel);
            return newExpenseModelList;
        }

        private static List<ExpenseModel> FilterByMonthAndCatagoryID(List<ExpenseModel> expenseModels, DateTime date, int catagoryID) {
            List<ExpenseModel> newExpenseModelList = new List<ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                if (expenseModel.Date.Month == date.Month && expenseModel.Date.Year == date.Year && expenseModel.CatagoryID == catagoryID)
                    newExpenseModelList.Add(expenseModel);
            return newExpenseModelList;
        }

        public static Dictionary<int, ExpenseModel> GetExpenseModelsDict(List<ExpenseModel> expenseModels) {
            Dictionary<int, ExpenseModel> ExpenseModelDict = new Dictionary<int, ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                ExpenseModelDict[expenseModel.ExpenseID] = expenseModel;
            return ExpenseModelDict;
        }

        public static decimal ConvertStringToDecimal(string input) {
            decimal amount = 0.00m;
            try { amount = Convert.ToDecimal(input); } catch { Debug.Log("[WARNING] Failed to convert String input to Decimal."); }
            return amount;
        }
    }
}
