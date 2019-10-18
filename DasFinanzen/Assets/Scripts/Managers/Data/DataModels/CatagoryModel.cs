

public class CatagoryModel : IModel {
    public int CatagoryID;
    public bool Reoccurring = false;
    public string NameText = "Default";
    public string ColorCode = "FFFFFF";

    public void Save() => Managers.Data.SaveLoad.SaveCatagoryModel(this);
    public void Delete() => Managers.Data.SaveLoad.DeleteCatagory(this);
}
