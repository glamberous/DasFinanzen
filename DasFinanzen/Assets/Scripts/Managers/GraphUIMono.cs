using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUIMono : MonoBehaviour {
    //[SerializeField] private GameObject OriginalColorBarObject = null;

    public GraphUIManager Manager { get; private set; }
    private void Awake() {
        Manager = new GraphUIManager();
        Manager.LoadMonoVariables();
    }
}

public class GraphUIManager : ManagerInterface {
    private GameObject OriginalColorBarObject = null;

    public void LoadMonoVariables() {

    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Graph manager starting...");

        status = ManagerStatus.Started;
    }
}
