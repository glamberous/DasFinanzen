
using System.Collections.Generic;
using System;
using System.Linq;
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

        public static Dictionary<int, ExpenseModel> GetExpenseModelsDict(List<ExpenseModel> expenseModels) {
            Dictionary<int, ExpenseModel> ExpenseModelDict = new Dictionary<int, ExpenseModel>();
            foreach (ExpenseModel expenseModel in expenseModels)
                ExpenseModelDict[expenseModel.ExpenseID] = expenseModel;
            return ExpenseModelDict;
        }

        public static decimal ConvertStringToDecimal(string input) {
            decimal amount = 0.00m;
            try { amount = Convert.ToDecimal(input); } 
            catch { Debug.Log("[WARNING] Failed to convert String input to Decimal."); }
            return amount;
        }
    }
}

