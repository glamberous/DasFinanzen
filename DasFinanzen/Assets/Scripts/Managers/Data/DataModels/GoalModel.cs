
using MessagePack;

[MessagePackObject]
public class GoalModel : IModel {
    public void Save() => Managers.Data.Queries.SaveGoalModel(this);
    public void Delete() => Managers.Data.Queries.DeleteGoalModel(this);

    [Key(0)]
    public decimal Amount = 1000.00m;
}
