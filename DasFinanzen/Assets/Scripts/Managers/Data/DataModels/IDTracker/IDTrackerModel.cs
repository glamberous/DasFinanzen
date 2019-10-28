
using UnityEngine;
using MsgPack.Serialization;

public class IDTrackerModel : IModel {

    public int CatagoryID = 0;

    public int ExpenseID = 0;

    public void Save() => Managers.Data.FileData.IDTrackerModel = this;
    public void Delete() => Debug.Log("[WARNING] Deleting IDTrackerModel is not allowed!");
}
