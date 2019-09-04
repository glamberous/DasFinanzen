using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUIManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    //[SerializeField] private GameObject OriginalColorBarObject = null;

    public void Startup() {
        Debug.Log("Graph manager starting...");

        status = ManagerStatus.Started;
    }
}
