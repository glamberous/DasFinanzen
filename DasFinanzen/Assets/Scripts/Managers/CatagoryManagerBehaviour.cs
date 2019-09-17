using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManagerBehaviour : MonoBehaviour {
    // Initialization Variables
    [SerializeField] private CatagoryBehaviour DailyOriginal = null;
    [SerializeField] private CatagoryBehaviour MonthlyOriginal = null;

    public CatagoryManager Manager { get; private set; }
    private void Awake() => Manager = new CatagoryManager(DailyOriginal, MonthlyOriginal);
}   

public class CatagoryManager : ManagerInterface { 
    // Variables for other classes to reference.
    public Dictionary<int, Catagory> Catagories = new Dictionary<int, Catagory>();

    private CatagoryBehaviour DailyOriginal = null;
    private CatagoryBehaviour MonthlyOriginal = null;

    public CatagoryManager(CatagoryBehaviour dailyOriginal, CatagoryBehaviour monthlyOriginal) {
        DailyOriginal = dailyOriginal;
        MonthlyOriginal = monthlyOriginal;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories(Managers.Data.CatagoryDataDict);
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, OnExpensesUpdated);

        status = ManagerStatus.Started;
    }

    private void InitializeCatagories(Dictionary<int, CatagoryData> catagoryDataDict) {
        TileUIData DailyUIData = new TileUIData(DailyOriginal.gameObject);
        TileUIData MonthlyUIData = new TileUIData(MonthlyOriginal.gameObject);
        foreach (KeyValuePair<int, CatagoryData> data in catagoryDataDict)
            if (data.Value.Reoccurring)
                Catagories.Add(data.Value.ID, InitializeCatagory(data.Value, MonthlyUIData));
            else
                Catagories.Add(data.Value.ID, InitializeCatagory(data.Value, DailyUIData));
        MonthlyUIData.UpdateTileSize();
        DailyUIData.UpdateTileSize();
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, TileUIData UIData) {
        CatagoryBehaviour newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original.GetComponent<CatagoryBehaviour>();
        else {
            newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryBehaviour>(), parent: UIData.Parent.transform) as CatagoryBehaviour;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Instance.Initialize(myCatagoryData);
        UIData.Count++;
        return newCatagory.Instance;
    }

    private void OnExpensesUpdated() {
        foreach (KeyValuePair<int, Catagory> catagory in Catagories)
            catagory.Value.UpdateExpensesTotal();
    }
}
