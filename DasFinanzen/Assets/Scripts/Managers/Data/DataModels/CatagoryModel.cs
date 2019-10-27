
using MessagePack;
using UnityEngine;

[MessagePackObject]
public class CatagoryModel : IModel {
    [Key(0)]
    public int CatagoryID { get; private set; } = IDTracker.CreateNew(IDType.CATAGORY);

    [Key(1)]
    public bool Recurring = false;

    [Key(2)]
    public string NameText = "Default";

    [Key(3)]
    public string ColorCode = "FFFFFF";

    public void Save() {
        if (IDTracker.IsNew(IDType.CATAGORY, CatagoryID))
            IDTracker.SaveID(IDType.CATAGORY, CatagoryID);
        else {
            CatagoryModel modelToDelete = UI.DataReformatter.GetCatagoryModel(Managers.Data.FileData.CatagoryModels, CatagoryID);
            Managers.Data.FileData.CatagoryModels.Remove(modelToDelete);
        }
        Managers.Data.FileData.CatagoryModels.Add(this);
        //Messenger.Broadcast(AppEvent.CATAGORIES_UPDATED);
    }

    public void Delete() => Debug.Log("[WARNING] Deleting CatagoryModel is not allowed!");
}
