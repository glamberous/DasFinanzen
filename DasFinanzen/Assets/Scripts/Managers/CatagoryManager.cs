using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {

    public ManagerStatus status { get; private set; }

    public void Startup() {
        Debug.Log("Data manager starting...");

        //Get and Load all catagory data here?
        //filename = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ManagerStatus.Started;
    }

}
