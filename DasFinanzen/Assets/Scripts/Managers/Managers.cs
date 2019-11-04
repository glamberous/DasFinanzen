using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(LocalizationManager))]
//Add new Managers to make them required

public class Managers : MonoBehaviour {
    public static DataManagerHumble Data { get; private set; }
    public static UIManagerHumble UI { get; private set; }
    public static LocalizationManagerHumble Locale { get; private set; }
    //Add More Managers here

    private List<IManager> startSequence;

    void Start() {
        DontDestroyOnLoad(gameObject);
        Data = GetComponent<DataManager>().Manager;
        UI = GetComponent<UIManager>().Manager;
        Locale = GetComponent<LocalizationManager>().Manager;
        //Add GetComponent for new Managers here

        startSequence = new List<IManager>();
        startSequence.Add(Data);
        startSequence.Add(Locale);
        startSequence.Add(UI);
        //Add More Managers to the list here

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers() {

        foreach (IManager manager in startSequence) {
            manager.Startup();
        }

        yield return null;

        int numModules = startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) {
            int lastReady = numReady;
            numReady = 0;

            foreach (IManager manager in startSequence) {
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

