using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.IO;

public class DataManagerBehaviour : MonoBehaviour {
    // Add any Data object to this Manager in the editor
    [SerializeField] private EditorData editorData = null;

    public DataManager Manager { get; private set; }
    private void Awake() => Manager = new DataManager(editorData);
}

public class DataManager : ManagerInterface {
    public FileData MyFileData { get; private set; } = null;
    public EditorData MyEditorData { get; private set; } = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");
        Load();
        status = ManagerStatus.Started;
    }

    public void Save() => SaveLoad.Save(MyFileData);
    public void Load() => MyFileData = SaveLoad.Load();
    public DataManager(EditorData editorData) => MyEditorData = editorData;

    private SaveLoadSystem SaveLoad = new SaveLoadSystem();









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
