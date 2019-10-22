
using MessagePack;
using UnityEngine;

[MessagePackObject]
public class IDTrackerModel : IModel {

    [Key(0)]
    public int CatagoryID = 0;

    [Key(1)]
    public int ExpenseID = 0;

    public void Save() => Managers.Data.FileData.IDTrackerModel = this;
    public void Delete() => Debug.Log("[WARNING] Deleting IDTrackerModel is not allowed!");
}
