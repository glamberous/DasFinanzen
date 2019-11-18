using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [HideInInspector] public UIManagerHumble Manager { get; private set; } = new UIManagerHumble();

    private void Awake() {
        SceneManager.sceneLoaded += Manager.OnSceneLoaded;
    }
}

public class UIManagerHumble : IManager { 
    private Dictionary<UI.WINDOW, GameObject> Windows = new Dictionary<WINDOW, GameObject>();
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Probably turn this into a switch when I get more scenes?
        if (scene.name == "Main")
            LoadMain();
    }

    private void LoadMain() {
        Windows = new Dictionary<WINDOW, GameObject>();
        foreach (IWindow windowObj in Resources.FindObjectsOfTypeAll(typeof(Window)) as IWindow[])
            Windows[windowObj.GetEnum()] = windowObj.GetGameObject();
        Push(UI.WINDOW.MAIN);
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("UI Manager starting...");

        status = ManagerStatus.Started;
        Debug.Log("UI Manager started.");
    }

    private Stack<IWindow> UIStack = new Stack<IWindow>();
    private float SortingOrder = 0;

    public void Push(UI.WINDOW window) {
        if (Windows.ContainsKey(window))
            UIStack.Push(Windows[window].GetComponent<IWindow>().Activate());
        else {
            Managers.Data.Runtime.DialogueWindowKey = 0;
            UIStack.Push(Windows[UI.WINDOW.DIALOGUE].GetComponent<IWindow>().Activate());
            Debug.Log($"[ERROR] Unnable to find Window {window.ToString()}.");
        }
        UIStack.Peek().SetZLayer(SortingOrder);
        SortingOrder -= 100;
    }

    public void Pop() {
        UIStack.Pop().Deactivate();
        SortingOrder += 100;
    }
}


