using System;
using UnityEngine;

public class ExpenseModel : IModel {
    public int ExpenseID { get; private set; } = -1;

    public DateTime Date = Managers.Data.Runtime.SelectedTime;

    public string NameText = "New Expense";

    public decimal Amount = 0.00m;

    public int CatagoryID { get; private set; } = Managers.Data.Runtime.CurrentCatagoryID;

    public CatagoryModel Catagory {
        get {
            if (_Catagory == null) {
                foreach (CatagoryModel catagoryModel in Managers.Data.FileData.CatagoryModels)
                    if (CatagoryID == catagoryModel.CatagoryID) {
                        _Catagory = catagoryModel;
                        return catagoryModel;
                    }
                return null;
            } else
                return _Catagory;
        }
    }

    private CatagoryModel _Catagory;

    public void Save() {
        if (ExpenseID == -1) {
            ExpenseID = IDTracker.CreateNew(IDType.EXPENSE);
            IDTracker.SaveID(IDType.EXPENSE, ExpenseID);
        } 
        else {
            ExpenseModel modelToRemove = UI.DataQueries.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
            Managers.Data.FileData.ExpenseModels.Remove(modelToRemove);
        }
        Managers.Data.FileData.ExpenseModels.Add(this);
        Managers.Data.Save();
        Messenger.Broadcast(UI.Events.EXPENSES_UPDATED);
    }

    public void Delete() {
        ExpenseModel modelToDelete = UI.DataQueries.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
        Managers.Data.FileData.ExpenseModels.Remove(modelToDelete);
        Managers.Data.Save();
        Messenger.Broadcast(UI.Events.EXPENSES_UPDATED);
    }
}