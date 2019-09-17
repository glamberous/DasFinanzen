using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManagerBehaviour : MonoBehaviour {
    //[SerializeField] private GameObject OriginalColorBarObject = null;

    public GraphManager Manager { get; private set; }
    private void Awake() => Manager = new GraphManager();
}

public class GraphManager : ManagerInterface {
    private GameObject OriginalColorBarObject = null;

    public GraphManager() {

    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Graph manager starting...");

        status = ManagerStatus.Started;
    }
}
