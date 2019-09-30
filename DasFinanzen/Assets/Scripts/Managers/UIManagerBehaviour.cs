using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerBehaviour : MonoBehaviour {
    // Add any UI Views to this manager in the Editor.
    [SerializeField] private Dictionary<string, ViewInterface> Views = null;

    public UIManager Manager { get; private set; }
    private void Awake() => Manager = new UIManager(editorData);
}

public class UIManager : ManagerInterface {
    public List<MVCObject> UIStack = new List<MVCObject>(); // Maybe don't use a List as a Stack? I forget if theres another data type to use.

    public void Push(string UIName) {
        if (!Views.ContainsKey(UIName))
            UIName = "Error"; //TODO create Error MVCObject.
        ViewInterface newView = Views[UIName];
        ModelInterface newModel = new ModelFactory(UIName);
        ControllerInterface newController = new ControllerFactory(UIName);
        MVCObject newUI = MVCObjectFactory(newModel, newView, newController); // MVCObject Factory should do all the initialization.
        UIStack.Add(newUI);
    }

    public void Pop() {
        // Disable and Remove the final object in the UIStack List
        MVCObject toRemove = UIStack[-1]; // Find a way to just do Pop(), then I can shorten this Method.
        toRemove.Disable();
        UIStack.Remove(toRemove);
    }

    private Dictionary<string, ViewInterface> Views = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("UI Manager starting...");

        foreach (KeyValuePair<string, ViewInterface> view in Views)
            view.Value.Disable();
        Push("Main");

        status = ManagerStatus.Started;
    }
}
