using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class GoalModel : IModel {
    [Key(0)]
    public decimal Amount = 1000.00m;

    public void Save() => Managers.Data.Queries.SaveGoalModel(this);
    public void Delete() => Managers.Data.Queries.DeleteGoalModel(this);
}
