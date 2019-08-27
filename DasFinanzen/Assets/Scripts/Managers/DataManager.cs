using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, ManagerInterface {

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
        gamestate.Add("expenses", Managers.Catagory.GetData());
        gamestate.Add("expenseGoal", Managers.ColorBar.GetData());

        FileStream stream = File.Create(filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
        Debug.Log("Data was saved.");
    }

    public void LoadGameState() {
        List<ExpenseData> expenses = new List<ExpenseData>();
        ExpenseData testExpense = new ExpenseData();
        testExpense.Amount = 666.66m;
        testExpense.EpochDate = 1566701026;
        testExpense.NameText = "Testing";
        testExpense.ID = 0;
        expenses.Add(testExpense);
        Managers.Catagory.LoadData(expenses);
        Managers.ColorBar.LoadData(700.00m);

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
}
