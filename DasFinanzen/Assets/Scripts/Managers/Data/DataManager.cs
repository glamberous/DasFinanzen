using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class DataManager : MonoBehaviour {
    [SerializeField] private List<CatagoryData> CatagoryDatas = null;

    private void Awake() {
        EditorData editorData = new EditorData();
        editorData.CatagoryDatas = CatagoryDatas;

        Manager = new DataManagerHumble(editorData);
    }
    public DataManagerHumble Manager { get; private set; }
}

public class DataManagerHumble : IManager {
    public DataManagerHumble(EditorData editorData) => EditorData = editorData;
    public EditorData EditorData { get; private set; } = null;
    public FileData FileData { get; private set; } = null;

    private SaveLoadSystem SaveLoad = new SaveLoadSystem();
    public void Save() => SaveLoad.Save(FileData);
    public void Load() => FileData = SaveLoad.Load();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");
        FileData = SaveLoad.Load();
        status = ManagerStatus.Started;
    }


    /*

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
    */
}
