
using UnityEngine;

public class DataManager : MonoBehaviour {
    private void Awake() => Manager = new DataManagerHumble();
    public DataManagerHumble Manager { get; private set; }
}

public class DataManagerHumble : IManager {
    public RuntimeData Runtime { get; private set; } = new RuntimeData();
    public FileData FileData { get; private set; } = new FileData();

    public void Save() => SaveLoad.SaveFileData(FileData);
    public void Load() => FileData = SaveLoad.LoadFileData();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");

        SaveLoad = new SaveLoadSystem();
        FileData = SaveLoad.LoadFileData();

        // TODO Figure out a way to Load Default Data better, this isn't scalable.
        DefaultDataGenerator.CatagoryModels();

        status = ManagerStatus.Started;
        Debug.Log("Data Manager started.");
    }

    private SaveLoadSystem SaveLoad = null;
}
