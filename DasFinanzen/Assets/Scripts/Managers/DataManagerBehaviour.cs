using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class DataManagerBehaviour : MonoBehaviour {
    // Catagories are loaded from the Unity Inspector, not the save profile. Gets converted into a Dictionary on App launch.
    [SerializeField] private List<CatagoryData> CatagoryDatas = null;

    public DataManager Manager { get; private set; }
    private void Awake() => Manager = new DataManager(CatagoryDatas);
}

public class DataManager : ManagerInterface {
    // The Data Variables to access during runtime. Catagorized by ID to work with more easily.
    public Dictionary<int, CatagoryData> CatagoryDataDict = new Dictionary<int, CatagoryData>();
    public Dictionary<int, List<ExpenseData>> ExpenseDatasDict = new Dictionary<int, List<ExpenseData>>();

    // Working variables for "Currently selected" data
    public int CurrentID = -1;
    public List<ExpenseData> CurrentExpenseDatas { get => ExpenseDatasDict.ContainsKey(CurrentID) ? ExpenseDatasDict[CurrentID] : null; }
    public CatagoryData CurrentCatagoryData { get => CatagoryDataDict.ContainsKey(CurrentID) ? CatagoryDataDict[CurrentID] : null; }

    public decimal BudgetGoal = 1000.00m;
    private string filename;

    public DataManager(List<CatagoryData> catagoryDatas) {
        LoadCatagories(catagoryDatas);
    }

    private void LoadCatagories(List<CatagoryData> catagoryDatas) {
        foreach (CatagoryData catagoryData in catagoryDatas)
            CatagoryDataDict[catagoryData.ID] = catagoryData;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data manager starting...");

        SetFilePath();
        LoadGameState();

        status = ManagerStatus.Started;
    }

    public void SetFilePath(string customDirectory = null) {
        filename = customDirectory == null ? Path.Combine(Application.persistentDataPath, "data.fin") : customDirectory;
    }

    public void LoadGameState() {
        Dictionary<string, object> gamestate = null;
        if (File.Exists(filename))
            gamestate = GetGamestateFromFile();
        if (gamestate != null)
            LoadFileValues(gamestate);
        else
            LoadDefaultValues();
    }

    private Dictionary<string, object> GetGamestateFromFile() {
        Dictionary<string, object> tempGamestate = null;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(filename, FileMode.Open);
        try {
            tempGamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        } catch {
            Debug.Log("Unable to Load Profile!");
        } finally {
            stream.Close();
        }
        return tempGamestate;
    }

    private void LoadFileValues(Dictionary<string, object> gamestate) {
        LoadExpenses(gamestate["expenses"] as List<ExpenseData>);
        LoadGoal((decimal)gamestate["expenseGoal"]);
        Debug.Log("Save Data was Loaded.");
    }

    private void LoadExpenses(List<ExpenseData> expenseDatas) {
        foreach (KeyValuePair<int, CatagoryData> catagoryData in CatagoryDataDict)
            ExpenseDatasDict[catagoryData.Key] = new List<ExpenseData>();
        foreach (ExpenseData expenseData in expenseDatas) {
            if (ExpenseDatasDict.ContainsKey(expenseData.ID))
                ExpenseDatasDict[expenseData.ID].Add(expenseData);
            else
                Debug.Log($"[WARNING] Catagory ID {expenseData.ID} was not found! Expense Data was lost.");
        }
    }

    private void LoadGoal(decimal goal) => BudgetGoal = goal;

    private void LoadDefaultValues() {
        LoadExpenses(new List<ExpenseData>());
        LoadGoal(1000.00m);
        Debug.Log("Default Data was Loaded.");
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

    private List<ExpenseData> ConvertExpensesForSave() {
        List<ExpenseData> ListToSave = new List<ExpenseData>();
        foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in ExpenseDatasDict)
            ListToSave.AddRange(expenseDatas.Value);
        return ListToSave;
    }

    public void AddExpense(ExpenseData newExpenseData) {
        ExpenseDatasDict[newExpenseData.ID].Add(newExpenseData);
        ExpensesUpdated();
    }

    public void EditExpense(ExpenseData oldExpenseData, ExpenseData newExpenseData) {
        if (oldExpenseData.ID != newExpenseData.ID) {
            Debug.Log("ERROR: Edit Command is not allowed to change Expense Data ID's. Edit Command was rejected.");
            return;
        }
        oldExpenseData.CopyData(newExpenseData);
        ExpensesUpdated();
    }

    public void RemoveExpense(ExpenseData delExpenseData) {
        ExpenseDatasDict[delExpenseData.ID].Remove(delExpenseData);
        ExpensesUpdated();
    }

    private void ExpensesUpdated() {
        Debug.Log("Data Updated.");
        SaveGameState();
        // Broadcast should always go after SaveGameState so that we ensure all app data is in a valid state before the UI starts updating.
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED); 
    }

    public decimal GetExpensesTotal(int ID) {
        decimal total = 0.00m;
        if (ExpenseDatasDict.ContainsKey(ID))
            foreach (ExpenseData expense in ExpenseDatasDict[ID])
                total += expense.Amount;
        return total;
    }
}
