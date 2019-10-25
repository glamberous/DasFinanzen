
using MessagePack;
using UnityEngine;

[MessagePackObject]
public class GoalModel : IModel {

    [Key(0)]
    public decimal Amount = 1000.00m;

    public void Save() {
        Managers.Data.FileData.GoalModel = this;
        Messenger.Broadcast(AppEvent.GOAL_UPDATED);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting GoalModel is not allowed!");
}
