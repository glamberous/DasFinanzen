using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IQueries {
    //Load
    List<CatagoryModel> GetCatagoryModels();
    List<ExpenseModel> GetExpenseModels(DateTime selectedTime, int id = -1);
    GoalModel GetGoalModel();

    //Save
    void SaveCatagoryModel(CatagoryModel myModel);
    void SaveExpenseModel(ExpenseModel myModel);
    void SaveGoalModel(GoalModel myModel);

    //Delete
    void DeleteCatagoryModel(CatagoryModel myModel);
    void DeleteExpenseModel(ExpenseModel expenseModel);
    void DeleteGoalModel(GoalModel myModel);
}

public class FileDataQueries : IQueries {
    public FileDataQueries(FileData fileData) => FileData = fileData;
    private FileData FileData;

    // ############################################## Load ##############################################
    #region Load

    public GoalModel GetGoalModel() => FileData.GoalModel;
    public List<CatagoryModel> GetCatagoryModels() => FileData.CatagoryModels;

    public List<ExpenseModel> GetExpenseModels(DateTime selectedTime, int catagoryID = -1) {
        return catagoryID == -1 ? GetExpenseModels_MonthOnly(selectedTime) : GetExpenseModels_MonthAndCatagoryID(selectedTime, catagoryID);
    }

    private List<ExpenseModel> GetExpenseModels_MonthOnly(DateTime selectedTime) {
        List<ExpenseModel> newExpenseModelList = new List<ExpenseModel>();
        foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
            if (expenseModel.Date.Month == selectedTime.Month && expenseModel.Date.Year == selectedTime.Year)
                newExpenseModelList.Add(expenseModel);
        return newExpenseModelList;
    }

    private List<ExpenseModel> GetExpenseModels_MonthAndCatagoryID(DateTime selectedTime, int catagoryID) {
        List<ExpenseModel> newExpenseModelList = new List<ExpenseModel>();
        foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
            if (expenseModel.Date.Month == selectedTime.Month && expenseModel.Date.Year == selectedTime.Year && expenseModel.CatagoryID == catagoryID)
                newExpenseModelList.Add(expenseModel);
        return newExpenseModelList;
    }

    #endregion
    // ############################################## Save ##############################################
    #region Save

    public void SaveGoalModel(GoalModel myModel) => FileData.GoalModel = myModel;

    public void SaveCatagoryModel(CatagoryModel myModel) {
        foreach (CatagoryModel catagoryModel in FileData.CatagoryModels)
            if (catagoryModel.CatagoryID == myModel.CatagoryID)
                FileData.CatagoryModels.Remove(catagoryModel);
        FileData.CatagoryModels.Add(myModel);
    }

    public void SaveExpenseModel(ExpenseModel myModel) {
        foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
            if (expenseModel.ExpenseID == myModel.ExpenseID)
                FileData.ExpenseModels.Remove(expenseModel);
        FileData.ExpenseModels.Add(myModel);
    }

    #endregion
    // ############################################# Delete #############################################
    #region Delete

    public void DeleteCatagoryModel(CatagoryModel myModel) => Debug.Log("[WARNING] Deleting CatagoryModel is not allowed!");

    public void DeleteExpenseModel(ExpenseModel myModel) {
        foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
            if (expenseModel.ExpenseID == myModel.ExpenseID)
                FileData.ExpenseModels.Remove(expenseModel);
    }

    public void DeleteGoalModel(GoalModel myModel) => Debug.Log("[WARNING] Deleting GoalModel is not allowed!");

    #endregion
}
