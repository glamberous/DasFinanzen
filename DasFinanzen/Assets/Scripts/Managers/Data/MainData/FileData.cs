
using System.Collections.Generic;

public class FileData {
    public IDTrackerModel IDTrackerModel = new IDTrackerModel();
    public List<CatagoryModel> CatagoryModels = new List<CatagoryModel>();
    public List<ExpenseModel> ExpenseModels = new List<ExpenseModel>();
    public List<GoalModel> GoalModels = new List<GoalModel>();
    public Localization.LOCALE Locale = Localization.LOCALE.EN;
}
