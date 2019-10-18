using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class GoalModel : IModel {
    public void Save() => Managers.Data.FileQueries.SaveGoal(this);
    public void Delete() => Debug.Log("[WARNING] GoalModel.Delete() is not allowed!");

    [Key(0)]
    public decimal Amount = 1000.00m;
}
