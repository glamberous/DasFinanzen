using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIDataReformatter {
    public static Dictionary<int, decimal> GetExpenseTotals(List<CatagoryModel> catagoryModels, List<ExpenseModel> expenseModels) {
        Dictionary<int, decimal> ExpenseTotals = new Dictionary<int, decimal>();
        foreach (CatagoryModel catagoryModel in catagoryModels)
            ExpenseTotals[catagoryModel.CatagoryID] = 0.00m;
        foreach (ExpenseModel expenseModel in expenseModels)
            ExpenseTotals[expenseModel.CatagoryID] += expenseModel.Amount;
        return ExpenseTotals;
    }

    public static Dictionary<int, CatagoryModel> SortCatagoryModels(List<CatagoryModel> catagoryModels) {
        Dictionary<int, CatagoryModel> CatagoryModelDict = new Dictionary<int, CatagoryModel>();
        foreach (CatagoryModel catagoryModel in catagoryModels)
            CatagoryModelDict[catagoryModel.CatagoryID] = catagoryModel;
        return CatagoryModelDict;
    }
}
