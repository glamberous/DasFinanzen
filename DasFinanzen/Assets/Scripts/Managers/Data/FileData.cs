
using MessagePack;
using System.Collections.Generic;

[MessagePackObject]
public class FileData {

    [Key(0)]
    public List<CatagoryModel> CatagoryModels = new List<CatagoryModel>();

    [Key(1)]
    public List<ExpenseModel> ExpenseModels = new List<ExpenseModel>();

    [Key(2)]
    public GoalModel GoalModel = new GoalModel();
}
