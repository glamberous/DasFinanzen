
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SaveAppData(AppData appData) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ProfileData.Fin";

        using (FileStream stream = File.Open(path, FileMode.Create))
            formatter.Serialize(stream, appData);
    }

    public static AppData LoadProfile() {
        string path = Application.persistentDataPath + "/ProfileData.Fin";
        AppData appData;
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Open(path, FileMode.Open)) {
                appData = formatter.Deserialize(stream) as AppData;
            }
            return appData;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null; // Change to default player profile
            //return AppData NewUserAppData;
        }
    }
} 
