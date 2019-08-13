using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(CatagoryManager))]
//Add new Managers to make them required

public class Managers : MonoBehaviour {
    public static DataManager Data { get; private set; }
    public static CatagoryManager Catagory { get; private set; }
    //Add More Managers here

    private List<ManagerInterface> startSequence;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        Data = GetComponent<DataManager>();
        Catagory = GetComponent<CatagoryManager>();
        //Add GetComponent for new Managers here

        startSequence = new List<ManagerInterface>();
        startSequence.Add(Data);
        startSequence.Add(Catagory);
        //Add More Managers to the list here

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {

        foreach (ManagerInterface manager in startSequence) {
            manager.Startup();
        }

        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach (ManagerInterface manager in startSequence) {
                if (manager.status == ManagerStatus.Started) {
                    numReady++;
                }
            }

            if (numReady > lastReady) {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }
            yield return null;
        }

        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
    }
}

