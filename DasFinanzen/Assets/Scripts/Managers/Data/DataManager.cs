
using UnityEngine;

public class DataManager : MonoBehaviour {
    private void Awake() => Manager = new DataManagerHumble();
    public DataManagerHumble Manager { get; private set; }
}

public class DataManagerHumble : IManager {
    public RuntimeData Runtime { get; private set; } = new RuntimeData();
    public FileData FileData { get => SaveLoad.FileData; }
    private SaveLoadSystem SaveLoad = null;

    public void Save() => SaveLoad.SaveFileData();
    public void Load() => SaveLoad.LoadFileData();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");

        SaveLoad = new SaveLoadSystem();
        SaveLoad.LoadFileData();

        status = ManagerStatus.Started;
        Debug.Log("Data Manager started.");
    }

    
}
