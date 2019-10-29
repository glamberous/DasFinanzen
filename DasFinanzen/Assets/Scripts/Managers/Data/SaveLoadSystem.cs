using System.IO;
using UnityEngine;
using MsgPack.Serialization;
using System;

public interface ISaveLoad {
    void LoadFileData();
    void SaveFileData(); 
}

public class SaveLoadSystem : ISaveLoad {
    public SaveLoadSystem() {
        SetFilePath();
    }

    public FileData FileData { get; private set; } = null;

    public void SetFilePath(string filename = "AppData.fin") => filepath = Application.persistentDataPath + $"/{filename}";
    private string filepath;

    private MessagePackSerializer serializer = MessagePackSerializer.Get<FileData>();
    
    public void LoadFileData() {
        bool DataSuccessfullyLoaded = false;
        if (File.Exists(filepath)) {
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open)) {
                try {
                    FileData = serializer.Unpack(fileStream) as FileData;
                    DataSuccessfullyLoaded = true;
                    Debug.Log(filepath + ": Successfully Loaded!");
                } catch {
                    Debug.Log(filepath + ": [ERROR] File Failed to Load!");
                }
            }
        } else
            Debug.Log(filepath + ": [WARNING] File not found!");

        if (!DataSuccessfullyLoaded) {
            Debug.Log("Loading Default Values into Save Profile...");
            FileData = new FileData();
            DefaultDataGenerator.LoadAll();
            Debug.Log("Default Values have been Loaded.");
        }
    }

    public void SaveFileData() {
        using (FileStream fileStream = new FileStream(filepath, FileMode.Create)) {
            try {
                serializer.Pack(fileStream, FileData);
                Debug.Log(filepath + ": Successfully Saved!");
            } catch {
                Debug.Log(filepath + ": [ERROR] File Failed to Save!");
            }
        }
    }
}
