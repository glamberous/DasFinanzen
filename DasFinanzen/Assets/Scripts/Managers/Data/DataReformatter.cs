using System.Collections;
using System.Collections.Generic;
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
    }
}

