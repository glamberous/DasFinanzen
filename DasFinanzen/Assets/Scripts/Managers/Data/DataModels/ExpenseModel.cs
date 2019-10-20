using System;
using MessagePack;

[MessagePackObject]
public class ExpenseModel : IModel {
    public void Save() => Managers.Data.Queries.SaveExpenseModel(this);
    public void Delete() => Managers.Data.Queries.DeleteExpenseModel(this);

    public int ExpenseID = Managers.Data.FileData.ExpenseModels.Count + 1;
    public DateTime Date = DateTime.Now;
    public string NameText = "New Expense";
    public decimal Amount = 0.00m;
    public int CatagoryID = Managers.Data.Runtime.CurrentCatagoryID;
}