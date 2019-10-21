
using UnityEngine;

public class DataManager : MonoBehaviour {
    private void Awake() => Manager = new DataManagerHumble();
    public DataManagerHumble Manager { get; private set; }
}

public class DataManagerHumble : IManager {
    public RuntimeData Runtime { get; private set; } = new RuntimeData();
    public IDTracker IDTracker { get; private set; } = null;
    public FileDataQueries Queries { get; private set; } = null;

    public UI.ModelCollector UIModelCollector { get; private set; } = null;
    // Add other Model Collectors here

    public void Save() => SaveLoad.SaveFileData(FileData);
    public void Load() => FileData = SaveLoad.LoadFileData();

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Data Manager starting...");

        SaveLoad = new SaveLoadSystem();
        FileData = SaveLoad.LoadFileData();
        Queries = new FileDataQueries(FileData);

        UIModelCollector = new UI.ModelCollector(Queries, Runtime);
        // Initiallize Data Loaders here

        IDTracker = new IDTracker(FileData.IDTrackerModel);

        status = ManagerStatus.Started;
    }

    private SaveLoadSystem SaveLoad = null;
    private FileData FileData = null;
    
}
