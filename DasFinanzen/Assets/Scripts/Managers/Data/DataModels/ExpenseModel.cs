﻿using System;

public class ExpenseModel : IModel {
    public int ExpenseID { get; private set; } = -1;

    public DateTime Date = DateTime.Now;

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
            ExpenseID = IDTracker.CreateNew(IDType.CATAGORY);
            IDTracker.SaveID(IDType.EXPENSE, CatagoryID);
        } 
        else {
            ExpenseModel modelToDelete = UI.DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
            Managers.Data.FileData.ExpenseModels.Remove(modelToDelete);
        }
        Managers.Data.FileData.ExpenseModels.Add(this);
        Managers.Data.Save();
        Messenger.Broadcast(UIEvent.EXPENSES_UPDATED);
    }

    public void Delete() {
        ExpenseModel modelToDelete = UI.DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
        Managers.Data.FileData.ExpenseModels.Remove(modelToDelete);
        Managers.Data.Save();
        Messenger.Broadcast(UIEvent.EXPENSES_UPDATED);
    }
}