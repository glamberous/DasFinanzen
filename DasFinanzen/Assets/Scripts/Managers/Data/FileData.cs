
using MessagePack;
using System.Collections.Generic;

[MessagePackObject]
public class FileData {

    [Key(0)]
    public List<CatagoryModel> CatagoryModels;

    [Key(1)]
    public List<ExpenseModel> ExpenseModels;
    
    [Key(2)]
    public GoalModel Goal;
}
