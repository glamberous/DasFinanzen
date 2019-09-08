using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ExpenseData : ICloneable {
    public long EpochDate;
    public string NameText;
    public decimal Amount;
    public int ID;

    public ExpenseData() {
        EpochDate = DateTimeOffset.Now.ToUnixTimeSeconds();
        Amount = 0.00m;
        ID = Managers.Data == null ? -1 : Managers.Data.CurrentID;
        NameText = ID == -1 ? "Default" : Managers.Data.CurrentCatagoryData.NameText;
    }
    
    public object Clone() {
        ExpenseData myClone = new ExpenseData();
        myClone.EpochDate = EpochDate;
        myClone.Amount = Amount;
        myClone.ID = ID;
        myClone.NameText = NameText;
        return myClone;
    }

    public void CopyData(ExpenseData data) {
        EpochDate = data.EpochDate;
        NameText = data.NameText;
        Amount = data.Amount;
        ID = data.ID;
    }

#if UNITY_EDITOR
    public void LoadTestData(long epochDate, string nameText, decimal amount, int id) {
        EpochDate = epochDate;
        NameText = nameText;
        Amount = amount;
        ID = id;
    }
#endif
}
