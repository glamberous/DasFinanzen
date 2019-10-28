
using UnityEngine;

public class CatagoryModel : IModel {

    public int CatagoryID { get; private set; } = -1;

    public bool Recurring = false;

    public string NameText = "Default";

    public string ColorCode = "FFFFFF";

    public void Save() {
        if (CatagoryID == -1) {
            CatagoryID = IDTracker.CreateNew(IDType.CATAGORY);
            IDTracker.SaveID(IDType.CATAGORY, CatagoryID);
        } 
        else {
            CatagoryModel modelToDelete = UI.DataReformatter.GetCatagoryModel(Managers.Data.FileData.CatagoryModels, CatagoryID);
            Managers.Data.FileData.CatagoryModels.Remove(modelToDelete);
        }
        Managers.Data.FileData.CatagoryModels.Add(this);
        Managers.Data.Save();
        //Messenger.Broadcast(AppEvent.CATAGORIES_UPDATED);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting CatagoryModel is not allowed!");
}
