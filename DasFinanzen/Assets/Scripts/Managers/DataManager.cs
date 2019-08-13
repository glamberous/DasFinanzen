﻿using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, ManagerInterface {

    public ManagerStatus status { get; private set; }
    private string filename;

    public void Startup() {
        Debug.Log("Data manager starting...");

        filename = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ManagerStatus.Started;
    }

    public void SaveGameState() {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        //gamestate.Add("daily", Managers.Catagory.GetData());
        //gamestate.Add("monthly", Managers.Catagory.GetData());

        FileStream stream = File.Create(filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    public void LoadGameState() {
        if (!File.Exists(filename)) {
            Debug.Log("No saved game");
            return;
        }

        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        //Managers.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
        //Managers.Player.UpdateData((int)gamestate["health"], (int)gamestate["maxHealth"]);
        //Managers.Mission.UpdateData((int)gamestate["curLevel"], (int)gamestate["maxLevel"]);
        //Managers.Mission.RestartCurrent();
    }
}