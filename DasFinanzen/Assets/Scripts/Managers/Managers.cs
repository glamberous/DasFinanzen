using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ErrorManagerBehaviour))]
[RequireComponent(typeof(CatagoryManagerBehaviour))]
[RequireComponent(typeof(ColorBarManagerBehaviour))]
[RequireComponent(typeof(GraphManagerBehaviour))]
[RequireComponent(typeof(DataManagerBehaviour))]
[RequireComponent(typeof(ExpenseManagerBehaviour))]
[RequireComponent(typeof(EditExpenseManagerBehaviour))]
//Add new Managers to make them required

public class Managers : MonoBehaviour {
    public static ErrorManager Error { get; private set; }
    public static DataManager Data { get; private set; }
    public static CatagoryManager CatagoryUI { get; private set; }
    public static ExpenseManager ExpenseUI { get; private set; }
    public static ColorBarManager ColorBarUI { get; private set; }
    public static GraphManager GraphUI { get; private set; }
    public static EditExpenseManager EditExpenseUI { get; private set; }
    //Add More Managers here

    private List<ManagerInterface> startSequence;

    void Start() {
        DontDestroyOnLoad(gameObject);
        Error = GetComponent<ErrorManagerBehaviour>().Manager;
        Data = GetComponent<DataManagerBehaviour>().Manager;
        CatagoryUI = GetComponent<CatagoryManagerBehaviour>().Manager;
        ExpenseUI = GetComponent<ExpenseManagerBehaviour>().Manager;
        ColorBarUI = GetComponent<ColorBarManagerBehaviour>().Manager;
        GraphUI = GetComponent<GraphManagerBehaviour>().Manager;
        EditExpenseUI = GetComponent<EditExpenseManagerBehaviour>().Manager;
        //Add GetComponent for new Managers here

        startSequence = new List<ManagerInterface>();
        startSequence.Add(Error);
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

