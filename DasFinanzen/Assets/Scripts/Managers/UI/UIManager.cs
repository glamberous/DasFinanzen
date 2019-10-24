using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class UIManager : MonoBehaviour {
    // Add any UI Views to this manager in the Editor.
    [SerializeField] private GameObject ErrorWindow = null;
    [SerializeField] private GameObject MainWindow = null;
    [SerializeField] private GameObject CatagoryWindow = null;
    [SerializeField] private GameObject ExpenseWindow = null;
    //[SerializeField] private GraphUIView GraphView = null;
    // Add new views here

    private void Awake() { // TODO change key's to Enums
        Dictionary<UI.WINDOW, GameObject> Windows = new Dictionary<UI.WINDOW, GameObject>();
        //Windows[UI.WINDOW.ERROR] = ErrorWindow;
        Windows[UI.WINDOW.MAIN] = MainWindow;
        Windows[UI.WINDOW.CATAGORY] = CatagoryWindow;
        Windows[UI.WINDOW.EXPENSE] = ExpenseWindow;
        //Windows["Graph"] = GraphWindow;
        // Add new views here

        Manager = new UIManagerHumble(Windows);
    }
    public UIManagerHumble Manager { get; private set; }
}

public class UIManagerHumble : IManager {
    public void Push(UI.WINDOW window) => UIStack.Push(Windows[window].GetComponent<IWindow>().Activate());
    public void Pop() => UIStack.Pop().Deactivate();

    public ManagerStatus status { get; private set; } 
    public void Startup() {
        Debug.Log("UI Manager starting...");

        foreach (KeyValuePair<UI.WINDOW, GameObject> view in Windows)
            view.Value.SetActive(false);
        Push(UI.WINDOW.MAIN);

        status = ManagerStatus.Started;
        Debug.Log("UI Manager started.");
    }

    private Dictionary<UI.WINDOW, GameObject> Windows = null;
    public UIManagerHumble(Dictionary<UI.WINDOW, GameObject> windows) => Windows = windows;

    private Stack<IWindow> UIStack = new Stack<IWindow>();
}
