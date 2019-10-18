using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Mono.Data.SqliteClient;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using UnityEngine;
using UI;
using MessagePack;

public interface ISaveLoad {
    FileData LoadFileData();
    void SaveFileData(FileData fileData); 

    //Load
    List<CatagoryModel> GetCatagories();
    List<ExpenseModel> GetExpenses(string date = "TimeDate", int id = -1);
    GoalModel GetGoal();

    //Save
    void SaveCatagory(CatagoryModel catagoryModel);
    void SaveExpense(ExpenseModel expenseModel);
    void SaveGoal(GoalModel goalModel);

    //Delete
    void DeleteExpense(ExpenseModel expenseModel);
}

public class SaveLoadSystem : ISaveLoad {
    private FileData FileData = null;

    public SaveLoadSystem() { SetFilePath(); }
    public void SetFilePath(string filename = "AppData.fin") => filepath = Application.persistentDataPath + $"/{filename}";
    private string filepath;

    public FileData LoadFileData() {
        byte[] rawData = File.ReadAllBytes(filepath);
        return MessagePackSerializer.Deserialize<FileData>(rawData);
    }

    public void SaveFileData(FileData fileData) {

    }

    // ############################################## Load ##############################################
    #region Load
    public List<CatagoryModel> GetCatagories() {
        return new List<CatagoryModel>();
    }

    public List<ExpenseModel> GetExpenses(string date = "TimeDate", int id = -1) {
        return new List<ExpenseModel>();
    }

    public GoalModel GetGoal() {
        return new GoalModel();
    }

    #endregion

    // ############################################## Save ##############################################
    #region Save
    public void SaveCatagory(CatagoryModel catagoryModel) {

    }

    public void SaveExpense(ExpenseModel expenseModel) {

    }

    public void SaveGoal(GoalModel goalModel) {
        FileData.Goal = goalModel;
    }

    #endregion

    // ############################################## Delete ##############################################
    #region Delete
    public void DeleteExpense(ExpenseModel expenseModel) {

    }

    #endregion
}
/*
public class SaveLoadSystem_v1 : ISaveLoad {
    public SaveLoadSystem_v1() { SetFilePath(); }

    private string filepath;
    public void SetFilePath(string customDirectory = null) {
        filepath = customDirectory == null ? Path.Combine(Application.persistentDataPath, "data.fin") : customDirectory;
    }

    public FileData Load() {
        FileData myData = new FileData();
        if (File.Exists(filepath)) {
            byte[] serializedData = File.ReadAllBytes(filepath);
            try {
                myData = MessagePackSerializer.Deserialize<FileData>(serializedData);
            } catch {
                Debug.Log("Unable to Load Profile!");
            }
        } else
            Debug.Log("File not found.");
        Debug.Log("Loading Default Values.");
        return myData;
    }

    public void Save(FileData fileData) {
        byte[] serializedData = MessagePackSerializer.Serialize(fileData);
        using (FileStream stream = File.Open(filepath, FileMode.Create)) {
            stream.Write(serializedData, 0, serializedData.Length);
        }
        Debug.Log("Data was saved.");
    }

    public void SaveJSON(FileData fileData) {

    }
}*/
