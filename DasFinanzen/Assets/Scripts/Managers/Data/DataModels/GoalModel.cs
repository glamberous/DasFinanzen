
using UnityEngine;
using System.Linq;

public class GoalModel : IModel  {

    public decimal Amount = 1000.00m;

    public string DateKey = "DEFAULT";

    public void Save() {
        foreach (GoalModel goalModel in Managers.Data.FileData.GoalModels)
            if (goalModel.DateKey == DateKey)
                Managers.Data.FileData.GoalModels.Remove(goalModel);
        Managers.Data.FileData.GoalModels.Add(this);
        Managers.Data.Save();
        Messenger.Broadcast(UIEvent.GOAL_UPDATED, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting GoalModel is not allowed!");
}
