using System;

public class ExpenseModel : ICloneable {
    public int ExpenseID;
    public long EpochDate = DateTimeOffset.Now.ToUnixTimeSeconds();
    public string NameText = "Default";
    public decimal Amount = 0.00m;
    public int CatagoryID;
    
    public object Clone() {
        ExpenseModel myClone = new ExpenseModel();
        myClone.EpochDate = EpochDate;
        myClone.Amount = Amount;
        myClone.ExpenseID = ExpenseID;
        myClone.NameText = NameText;
        return myClone;
    }

    public void CopyFrom(ExpenseModel data) {
        EpochDate = data.EpochDate;
        NameText = data.NameText;
        Amount = data.Amount;
        ExpenseID = data.ExpenseID;
    }
}
