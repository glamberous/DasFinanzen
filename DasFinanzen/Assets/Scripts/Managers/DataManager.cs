using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, ManagerInterface {
    [SerializeField] private List<CatagoryData> CatagoryDatas = null;

    [HideInInspector] public Dictionary<int, CatagoryData> CatagoryDataDict = new Dictionary<int, CatagoryData>();
    [HideInInspector] public Dictionary<int, List<ExpenseData>> ExpenseDatasDict = new Dictionary<int, List<ExpenseData>>();

    [HideInInspector] public int CurrentID = -1;
    [HideInInspector] public List<ExpenseData> CurrentExpenseDatas { get => ExpenseDatasDict[CurrentID]; }
    [HideInInspector] public CatagoryData CurrentCatagoryData { get => CatagoryDataDict[CurrentID]; }

    [HideInInspector] public decimal BudgetGoal = 1000.00m;
    
    public ManagerStatus status { get; private set; }
    private string filename;

    public void Startup() {
        Debug.Log("Data manager starting...");

        filename = Path.Combine(Application.persistentDataPath, "data.fin");
        Managers.Data.LoadGameState();
        

        status = ManagerStatus.Started;
    }

    public void SaveGameState() {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        //gamestate.Add("expenses", Managers.ExpenseUI.GetData());
        //gamestate.Add("expenseGoal", Managers.ColorBarUI.GetData());

        FileStream stream = File.Create(filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
        Debug.Log("Data was saved.");
    }

    public void LoadGameState() {
        LoadCatagories();
        
        List<ExpenseData> expenses = new List<ExpenseData>();
        ExpenseData testExpense = new ExpenseData();
        testExpense.Amount = 600.00m;
        testExpense.EpochDate = 1567273880;
        testExpense.NameText = "Testing";
        testExpense.ID = 0;
        expenses.Add(testExpense);

        LoadExpenses(expenses);

        //Managers.Expense.LoadData(expenses);
        //Managers.ColorBar.LoadData(700.00m);


        /*
        if (!File.Exists(filename)) {
            Debug.Log("No saved game");
            return;
        }

        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        Managers.Catagory.UpdateData(gamestate["catagories"] as List<CatagoryData>, gamestate["expenses"] as List<ExpenseData>);
        */
        Debug.Log("Data was Loaded.");
    }

    private void LoadCatagories() {
        foreach (CatagoryData catagoryData in CatagoryDatas)
            CatagoryDataDict[catagoryData.ID] = catagoryData;
    }

    private void LoadExpenses(List<ExpenseData> expenseDatas) {
        foreach (ExpenseData expenseData in expenseDatas) {
            if (!ExpenseDatasDict.ContainsKey(expenseData.ID))
                ExpenseDatasDict[expenseData.ID] = new List<ExpenseData>();
            ExpenseDatasDict[expenseData.ID].Add(expenseData);
        }
    }

    public decimal GetExpensesTotal(int ID) {
        decimal total = 0.00m;
        if (ExpenseDatasDict.ContainsKey(ID))
            foreach (ExpenseData expense in ExpenseDatasDict[ID])
                total += expense.Amount;
        return total;
    }

    public void AddExpense() { }

    public void DeleteExpense() { }

    public float GetWidthBasedOffPercentOfScreenWidth(int ID) => ((float)GetExpensesTotal(ID) / (float)BudgetGoal) * Screen.width;

    
    
    /*
    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatasToExport = new List<ExpenseData>();
        foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in ExpenseDatasDict)
            expenseDatasToExport.AddRange(expenseDatas.Value);
        return expenseDatasToExport;
    }*/
}
