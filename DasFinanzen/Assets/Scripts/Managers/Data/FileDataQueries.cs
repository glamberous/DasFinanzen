using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQueries {
    //Load
    List<CatagoryModel> GetCatagories();
    List<ExpenseModel> GetExpenses(string date = "TimeDate", int id = -1);
    GoalModel GetGoal();

    //Save
    void SaveCatagory(CatagoryModel catagoryModel);
    void SaveExpense(ExpenseModel expenseModel);
    void SaveGoal(GoalModel goalModel);

    //Delete
    void DeleteExpense(ExpenseModel expenseModel);
}

public class FileDataQueries : IQueries {
    public FileDataQueries(FileData fileData) => FileData = fileData;
    private FileData FileData;

    // ############################################## Load ##############################################
    public List<CatagoryModel> GetCatagories() {

        return new List<CatagoryModel>();
    }

    public List<ExpenseModel> GetExpenses(string date = "TimeDate", int id = -1) {

        return new List<ExpenseModel>();
    }

    public GoalModel GetGoal() {

        return new GoalModel();
    }

    //Save
    public void SaveCatagory(CatagoryModel catagoryModel) {

    }

    public void SaveExpense(ExpenseModel expenseModel) {

    }

    public void SaveGoal(GoalModel goalModel) {

    }

    //Delete
    public void DeleteExpense(ExpenseModel expenseModel) {

    }
}
