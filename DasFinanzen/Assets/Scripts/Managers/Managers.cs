using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CatagoryUIMono))]
[RequireComponent(typeof(ColorBarUIMono))]
[RequireComponent(typeof(GraphUIMono))]
[RequireComponent(typeof(DataMono))]
[RequireComponent(typeof(ExpenseUIMono))]
[RequireComponent(typeof(EditExpenseUIMono))]
//Add new Managers to make them required

public class Managers : MonoBehaviour {
    public static DataManager Data { get; private set; }
    public static CatagoryUIManager CatagoryUI { get; private set; }
    public static ExpenseUIManager ExpenseUI { get; private set; }
    public static ColorBarUIManager ColorBarUI { get; private set; }
    public static GraphUIManager GraphUI { get; private set; }
    public static EditExpenseUIManager EditExpenseUI { get; private set; }
    //Add More Managers here

    private List<ManagerInterface> startSequence;

    void Start() {
        DontDestroyOnLoad(gameObject);

        Data = GetComponent<DataMono>().Manager;
        CatagoryUI = GetComponent<CatagoryUIMono>().Manager;
        ExpenseUI = GetComponent<ExpenseUIMono>().Manager;
        ColorBarUI = GetComponent<ColorBarUIMono>().Manager;
        GraphUI = GetComponent<GraphUIMono>().Manager;
        EditExpenseUI = GetComponent<EditExpenseUIMono>().Manager;
        //Add GetComponent for new Managers here

        startSequence = new List<ManagerInterface>();
        startSequence.Add(Data);
        startSequence.Add(CatagoryUI);
        startSequence.Add(ExpenseUI);
        startSequence.Add(ColorBarUI);
        startSequence.Add(GraphUI);
        startSequence.Add(EditExpenseUI);
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

