using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ColorBarManagerBehaviour : MonoBehaviour {
    [SerializeField] private ColorBar OriginalColorBar = null;
    [SerializeField] private Canvas MyCanvas = null;

    public ColorBarManager Manager { get; private set; }
    private void Awake() => Manager = new ColorBarManager(OriginalColorBar, MyCanvas.GetComponent<RectTransform>());
}

public class ColorBarManager : ManagerInterface {
    private Dictionary<int, ColorBar> ColorBarDict = new Dictionary<int, ColorBar>();
    private ColorBar OriginalColorBar = null;
    private RectTransform CanvasRect = null;

    public ColorBarManager(ColorBar originalColorBar, RectTransform canvasRect) {
        OriginalColorBar = originalColorBar;
        CanvasRect = canvasRect;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("ColorBar manager starting...");

        InitializeColorBar();
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, UpdateColorBar);
        Messenger.AddListener(AppEvent.GOAL_UPDATED, UpdateColorBar);

        status = ManagerStatus.Started;
    }

    

    
}
*/
