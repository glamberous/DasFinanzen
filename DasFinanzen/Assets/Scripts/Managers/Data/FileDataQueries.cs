using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IQueries {
    //Load
    List<CatagoryModel> GetCatagoryModels();
    List<ExpenseModel> GetExpenseModels(string date = "TimeDate", int id = -1);
    GoalModel GetGoalModel();

    //Save
    void SaveCatagoryModel(CatagoryModel myModel);
    void SaveExpenseModel(ExpenseModel myModel);
    void SaveGoalModel(GoalModel myModel);

    //Delete
    void DeleteExpenseModel(ExpenseModel expenseModel);
}

public class FileDataQueries : IQueries {
    public FileDataQueries(FileData fileData) => FileData = fileData;
    private FileData FileData;

    // ############################################## Load ##############################################
    #region Load

    public GoalModel GetGoalModel() => FileData.GoalModel;
    public List<CatagoryModel> GetCatagoryModels() => FileData.CatagoryModels;

    public List<ExpenseModel> GetExpenseModels(Month month, int catagoryID = -1) {
        if (catagoryID == -1)
            foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
                if expenseModel.EpochDate.
        
        return new List<ExpenseModel>();
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

    public void DeleteExpenseModel(ExpenseModel myModel) {
        foreach (ExpenseModel expenseModel in FileData.ExpenseModels)
            if (expenseModel.ExpenseID == myModel.ExpenseID)
                FileData.ExpenseModels.Remove(expenseModel);
    }

    #endregion
}
