
using MessagePack;

[MessagePackObject]
public class IDTrackerModel : IModel {
    public void Save() => Managers.Data.Queries.SaveIDTrackerModel(this);
    public void Delete() => Managers.Data.Queries.DeleteIDTrackerModel(this);

    [Key(0)]
    public int CatagoryID = 0;

    [Key(1)]
    public int ExpenseID = 0;
}
