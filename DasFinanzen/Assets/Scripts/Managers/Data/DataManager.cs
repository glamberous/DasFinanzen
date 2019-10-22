
using UnityEngine;

public class DataManager : MonoBehaviour {
    private void Awake() => Manager = new DataManagerHumble();
    public DataManagerHumble Manager { get; private set; }
}

public class DataManagerHumble : IManager {
    public RuntimeData Runtime { get; private set; } = new RuntimeData();
    public FileData FileData { get; private set; } = null;

    public void Save() => SaveLoad.SaveFileData(FileData);
    public void Load() => FileData = SaveLoad.LoadFileData();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");

        SaveLoad = new SaveLoadSystem();
        FileData = SaveLoad.LoadFileData();
        DefaultDataGenerator.CatagoryModels();

        status = ManagerStatus.Started;
    }

    private SaveLoadSystem SaveLoad = null;

    
}
