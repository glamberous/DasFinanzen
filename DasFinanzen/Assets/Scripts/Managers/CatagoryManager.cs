using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    // Variables for other classes to reference.
    [HideInInspector] public Dictionary<int, Catagory> Catagories = new Dictionary<int, Catagory>();
    [HideInInspector] public Catagory CurrentCatagory { get => Catagories[CurrentID]; }
    [HideInInspector] public int CurrentID = -1;

    // Initialization Variables
    [SerializeField] private CatagoryData[] CatagoryDatas = null;
    [SerializeField] private Catagory DailyOriginal = null;
    [SerializeField] private Catagory MonthlyOriginal = null;

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
        foreach (CatagoryData data in CatagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(data.ID, InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(data.ID, InitializeCatagory(data, DailyUIData));
        MonthlyUIData.UpdateTileSize();
        DailyUIData.UpdateTileSize();
    }

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, TileUIData UIData) {
        Catagory newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original.GetComponent<Catagory>();
        else {
            newCatagory = Instantiate(original: UIData.Original.GetComponent<Catagory>(), parent: UIData.Parent.transform) as Catagory;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (Constants.CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Construct(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }

    private void OnDestroy() => Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, OnExpensesUpdated);
    private void OnExpensesUpdated() {
        foreach (KeyValuePair<int, Catagory> catagory in Catagories)
            catagory.Value.UpdateExpensesTotal();
    }
}
