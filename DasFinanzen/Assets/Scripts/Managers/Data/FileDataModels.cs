using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFileDataModels {
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

public class FileDataQueries {



    // ############################################## Load ##############################################
    public static List<CatagoryModel> GetCatagories() {

        return new List<CatagoryModel>();
    }

    public static List<ExpenseModel> GetExpenses(string date = "TimeDate", int id = -1) {

        return new List<ExpenseModel>();
    }

    public static GoalModel GetGoal() {

        return new GoalModel();
    }

    //Save
    public static void SaveCatagory(CatagoryModel catagoryModel) {

    }

    public static void SaveExpense(ExpenseModel expenseModel) {

    }

    public static void SaveGoal(GoalModel goalModel) {

    }

    //Delete
    public static void DeleteExpense(ExpenseModel expenseModel) {

    }
}
