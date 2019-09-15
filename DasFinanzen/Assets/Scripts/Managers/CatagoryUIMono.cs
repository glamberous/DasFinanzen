using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryUIMono : MonoBehaviour {
    // Initialization Variables
    [SerializeField] private CatagoryMono DailyOriginal = null;
    [SerializeField] private CatagoryMono MonthlyOriginal = null;

    public CatagoryUIManager Manager { get; private set; }
    private void Awake() => Manager = new CatagoryUIManager(DailyOriginal, MonthlyOriginal);
}   

public class CatagoryUIManager : ManagerInterface { 
    // Variables for other classes to reference.
    public Dictionary<int, Catagory> CatagoryUIs = new Dictionary<int, Catagory>();

    private CatagoryMono DailyOriginal = null;
    private CatagoryMono MonthlyOriginal = null;

    public CatagoryUIManager(CatagoryMono dailyOriginal, CatagoryMono monthlyOriginal) {
        DailyOriginal = dailyOriginal;
        MonthlyOriginal = monthlyOriginal;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories();
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, OnExpensesUpdated);

        status = ManagerStatus.Started;
    }

    private void InitializeCatagories() {
        TileUIData DailyUIData = new TileUIData(DailyOriginal.gameObject);
        TileUIData MonthlyUIData = new TileUIData(MonthlyOriginal.gameObject);
        foreach (KeyValuePair<int, CatagoryData> data in Managers.Data.CatagoryDataDict)
            if (data.Value.Reoccurring)
                CatagoryUIs.Add(data.Value.ID, InitializeCatagory(data.Value, MonthlyUIData));
            else
                CatagoryUIs.Add(data.Value.ID, InitializeCatagory(data.Value, DailyUIData));
        MonthlyUIData.UpdateTileSize();
        DailyUIData.UpdateTileSize();
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, TileUIData UIData) {
        CatagoryMono newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original.GetComponent<CatagoryMono>();
        else {
            newCatagory = GameObject.Instantiate(original: UIData.Original.GetComponent<CatagoryMono>(), parent: UIData.Parent.transform) as CatagoryMono;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Instance.Initialize(myCatagoryData);
        UIData.Count++;
        return newCatagory.Instance;
    }

    private void OnExpensesUpdated() {
        foreach (KeyValuePair<int, Catagory> catagory in CatagoryUIs)
            catagory.Value.UpdateExpensesTotal();
    }
}
