
using UnityEngine;
using System.Linq;

public class GoalModel : IModel  {

    public decimal Amount = 1000.00m;

    public string DateKey = "DEFAULT";

    public void Save() {
        GoalModel ModelToRemove = UI.DataQueries.GetGoalModel(Managers.Data.FileData.GoalModels, Managers.Data.Runtime.SelectedTime);
        Managers.Data.FileData.GoalModels.Remove(ModelToRemove);
        Managers.Data.FileData.GoalModels.Add(this);
        Managers.Data.Save();
        Messenger.Broadcast(UI.Events.GOAL_UPDATED, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting GoalModel is not allowed!");
}
