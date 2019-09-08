using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class DataManager : MonoBehaviour, ManagerInterface {
    // Catagories are loaded from the Unity Inspector, not the save profile. Gets converted into a Dictionary on App launch.
    [SerializeField] private List<CatagoryData> CatagoryDatas = null;

    // The Data Variables to access during runtime. Catagorized by ID to work with more easily.
    [HideInInspector] public Dictionary<int, CatagoryData> CatagoryDataDict = new Dictionary<int, CatagoryData>();
    [HideInInspector] public Dictionary<int, List<ExpenseData>> ExpenseDatasDict = new Dictionary<int, List<ExpenseData>>();

    // Working variables for "Currently selected" data
    [HideInInspector] public int CurrentID = -1;
    [HideInInspector] public List<ExpenseData> CurrentExpenseDatas { get => ExpenseDatasDict[CurrentID] ?? null; }
    [HideInInspector] public CatagoryData CurrentCatagoryData { get => CatagoryDataDict[CurrentID] ?? null; }

    [HideInInspector] public decimal BudgetGoal = 1000.00m;


    public ManagerStatus status { get; private set; }
    private string filename;

    public void Startup() {
        Debug.Log("Data manager starting...");

        filename = Path.Combine(Application.persistentDataPath, "data.fin");
        LoadGameState();
        
        status = ManagerStatus.Started;
    }

    public void SaveGameState() {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("expenses", ConvertExpensesForSave());
        gamestate.Add("expenseGoal", BudgetGoal);

        FileStream stream = File.Create(filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
        Debug.Log("Data was saved.");
    }

    public void LoadGameState() {
        if (File.Exists(filename)) {
            Dictionary<string, object> gamestate;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(filename, FileMode.Open);
            try {
                gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
            } catch {
                Debug.Log("Unable to Load Profile!");
                LoadDefaultValues();
            } finally {
                stream.Close();
            }
            LoadCatagories();
            LoadExpenses(gamestate["expenses"] as List<ExpenseData>);
            LoadGoal((decimal)gamestate["expenseGoal"]);

            Debug.Log("Save Data was Loaded.");
        }
        else {
            Debug.Log("No saved game");
            LoadDefaultValues();
        }
    }

    private void LoadDefaultValues() {
        LoadCatagories();
        LoadExpenses(new List<ExpenseData>());
        LoadGoal(1000.00m);
        Debug.Log("Default Data was Loaded.");
    }

    private List<ExpenseData> ConvertExpensesForSave() {
        List<ExpenseData> ListToSave = new List<ExpenseData>();
        foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in ExpenseDatasDict)
            ListToSave.AddRange(expenseDatas.Value);
        return ListToSave;
    }

    private void LoadCatagories() {
        foreach (CatagoryData catagoryData in CatagoryDatas)
            CatagoryDataDict[catagoryData.ID] = catagoryData;
    }

    private void LoadExpenses(List<ExpenseData> expenseDatas) {
        foreach (KeyValuePair<int, CatagoryData> catagoryData in CatagoryDataDict)
            ExpenseDatasDict[catagoryData.Key] = new List<ExpenseData>();
        foreach (ExpenseData expenseData in expenseDatas) {
            ExpenseDatasDict[expenseData.ID].Add(expenseData);
        }
    }

    private void LoadGoal(decimal goal) => BudgetGoal = goal;

    public void AddExpense(ExpenseData newExpenseData) {
        ExpenseDatasDict[newExpenseData.ID].Add(newExpenseData);
        Debug.Log("Data Updated.");
        SaveGameState();
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
    }

    public void RemoveExpense(ExpenseData delExpenseData) {
        ExpenseDatasDict[delExpenseData.ID].Remove(delExpenseData);
        Debug.Log("Data Updated.");
        SaveGameState();
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
    }

    public float GetWidthBasedOffPercentOfScreenWidth(int ID) {
        float calculation = ((float)GetExpensesTotal(ID) / (float)BudgetGoal) * 337.5f; // TODO - Bug here with needing to reference the Canvas Width, not screen width.
        return calculation;
    }

    public decimal GetExpensesTotal(int ID) {
        decimal total = 0.00m;
        if (ExpenseDatasDict.ContainsKey(ID))
            foreach (ExpenseData expense in ExpenseDatasDict[ID])
                total += expense.Amount;
        return total;
    }
}
