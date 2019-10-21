using System;
using MessagePack;

[MessagePackObject]
public class ExpenseModel : IModel {
    public void Save() => Managers.Data.Queries.SaveExpenseModel(this);
    public void Delete() => Managers.Data.Queries.DeleteExpenseModel(this);

    [Key(0)]
    public int ExpenseID { get; private set; } = Managers.Data.IDTracker.CreateNew(IDType.EXPENSE);

    [Key(1)]
    public DateTime Date = DateTime.Now;

    [Key(2)]
    public string NameText = "New Expense";

    [Key(3)]
    public decimal Amount = 0.00m;

    [Key(4)]
    public int CatagoryID = Managers.Data.Runtime.CurrentCatagoryID;
}