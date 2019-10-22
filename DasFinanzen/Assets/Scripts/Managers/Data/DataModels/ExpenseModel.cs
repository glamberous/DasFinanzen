﻿using System;
using MessagePack;

[MessagePackObject]
public class ExpenseModel : IModel {
    [Key(0)]
    public int ExpenseID { get; private set; } = IDTracker.CreateNew(IDType.EXPENSE);

    [Key(1)]
    public DateTime Date = DateTime.Now;

    [Key(2)]
    public string NameText = "New Expense";

    [Key(3)]
    public decimal Amount = 0.00m;

    [Key(4)]
    public int CatagoryID = Managers.Data.Runtime.CurrentCatagoryID;

    public void Save() {
        if (IDTracker.IsNew(IDType.EXPENSE, ExpenseID))
            IDTracker.SaveID(IDType.EXPENSE, ExpenseID);
        else
            foreach (ExpenseModel expenseModel in Managers.Data.FileData.ExpenseModels)
                if (expenseModel.ExpenseID == ExpenseID)
                    Managers.Data.FileData.ExpenseModels.Remove(expenseModel);
        Managers.Data.FileData.ExpenseModels.Add(this);
    }

    public void Delete() {
        foreach (ExpenseModel expenseModel in Managers.Data.FileData.ExpenseModels)
            if (expenseModel.ExpenseID == ExpenseID)
                Managers.Data.FileData.ExpenseModels.Remove(expenseModel);
    }
}