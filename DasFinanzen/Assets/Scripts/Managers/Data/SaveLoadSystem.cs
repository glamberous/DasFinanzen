using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Mono.Data.SqliteClient;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;
using UI;

public interface ISaveLoad {
    List<CatagoryModel> GetCatagoryModels();
    List<ExpenseModel> GetExpenseModels(string time = "CurrentTime", int id = -1);
}

public class SaveLoadSystem_SQLite : ISaveLoad {

    public SaveLoadSystem_SQLite() { SetFilePath(); }
    public void SetFilePath(string filename = "AppData.db") => filepath = "Data Source=" + Application.persistentDataPath + $"/{filename};Version=3;";
    private string filepath;

    private string LoadConnectionString(string id = "Default") => ConfigurationManager.ConnectionStrings[id].ConnectionString;

    public List<CatagoryModel> GetCatagoryModels() {
        /*
        using (IDbConnection connection = new SqliteConnection() {
            return connection.Query<CatagoryModel>("SELECT *", null).ToList();
        } */

        return new List<CatagoryModel>();
    }

    public List<ExpenseModel> GetExpenseModels(string time = "CurrentTime", int id = -1) {

        return new List<ExpenseModel>();
    }
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