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
}

public class SaveLoadSystem : ISaveLoad {
    private FileData FileData = null;

    public SaveLoadSystem() { SetFilePath(); }
    public void SetFilePath(string filename = "AppData.fin") => filepath = Application.persistentDataPath + $"/{filename}";
    private string filepath;

    public FileData LoadFileData() {
        FileData myData = new FileData();
        if (File.Exists(filepath)) {
            byte[] serializedData = File.ReadAllBytes(filepath);
            try {
                myData = MessagePackSerializer.Deserialize<FileData>(serializedData);
            } catch {
                Debug.Log("Unable to Load Profile! \nLoading Default Values.");
            }
        } else
            Debug.Log("File not found. \nLoading Default Values.");
        return myData;
    }

    public void SaveFileData(FileData fileData) {
        byte[] rawData = MessagePackSerializer.Serialize(fileData);
        File.WriteAllBytes(filepath, rawData);
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
