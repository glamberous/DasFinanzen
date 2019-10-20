﻿
using MessagePack;

[MessagePackObject]
public class CatagoryModel : IModel {

    [Key(0)]
    public int CatagoryID { get; private set; } = Managers.Data.IDTracker.CreateNew(UniqueIDs.CATAGORY);

    [Key(1)]
    public bool Reoccurring = false;

    [Key(2)]
    public string NameText = "Default";

    [Key(3)]
    public string ColorCode = "FFFFFF";

    public void Save() => Managers.Data.Queries.SaveCatagoryModel(this);
    public void Delete() => Managers.Data.Queries.DeleteCatagoryModel(this);
}
