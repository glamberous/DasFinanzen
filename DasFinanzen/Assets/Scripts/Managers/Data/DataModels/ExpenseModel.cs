using System;
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
    public int CatagoryID { get; private set; } = Managers.Data.Runtime.CurrentCatagoryID;

    [IgnoreMember]
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

    [IgnoreMember]
    private CatagoryModel _Catagory;

    public void Save() {
        if (IDTracker.IsNew(IDType.EXPENSE, ExpenseID))
            IDTracker.SaveID(IDType.EXPENSE, ExpenseID);
        else {
            ExpenseModel modelToDelete = UI.DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
            Managers.Data.FileData.ExpenseModels.Remove(modelToDelete);
        }
        Managers.Data.FileData.ExpenseModels.Add(this);
        Messenger.Broadcast(UIEvent.EXPENSES_UPDATED);
    }

    public void Delete() {
        ExpenseModel modelToDelete = UI.DataReformatter.GetExpenseModel(Managers.Data.FileData.ExpenseModels, ExpenseID);
        Managers.Data.FileData.ExpenseModels.Remove(modelToDelete);
        Messenger.Broadcast(UIEvent.EXPENSES_UPDATED);
    }
}