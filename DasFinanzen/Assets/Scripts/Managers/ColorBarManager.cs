using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    [SerializeField] private GameObject OriginalColorBarObject = null;

    public void Startup() {
        Debug.Log("ColorBar manager starting...");
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, Placeholder);

        status = ManagerStatus.Started;
    }

    public void Placeholder() { }
}
