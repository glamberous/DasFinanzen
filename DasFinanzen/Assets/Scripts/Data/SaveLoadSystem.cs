using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MessagePack;

public class SaveLoadSystem {
    public SaveLoadSystem() { SetFilePath(); }

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
        }
        else
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
}
