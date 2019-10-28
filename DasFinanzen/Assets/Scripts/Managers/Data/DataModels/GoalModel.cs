
using UnityEngine;
using MsgPack.Serialization;

public class GoalModel : IModel  {

    public decimal Amount = 1000.00m;

    public void Save() {
        Managers.Data.FileData.GoalModel = this;
        Messenger.Broadcast(UIEvent.GOAL_UPDATED);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting GoalModel is not allowed!");
}
