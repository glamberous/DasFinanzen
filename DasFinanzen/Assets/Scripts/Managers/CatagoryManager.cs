using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    public List<Catagory> Catagories = new List<Catagory>();

    private const float CatagoryOffset = 30.0f;
    [SerializeField] private Catagory DailyOriginal = null;
    private CatagoryUIData DailyUIData = null;

    [SerializeField] private Catagory MonthlyOriginal = null;
    private CatagoryUIData MonthlyUIData = null;

    public void Startup() {
        Debug.Log("Data manager starting...");

        DailyUIData = new CatagoryUIData(DailyOriginal);
        MonthlyUIData = new CatagoryUIData(MonthlyOriginal);
        //Get and Load all catagory data here?
        //filename = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ManagerStatus.Started;
    }
    
    public void UpdateData(List<CatagoryData> catagoryDatas, List<ExpenseData> expenseDatas) {
        foreach (CatagoryData data in catagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(InitializeCatagory(data, DailyUIData));
        UpdateTileSize(MonthlyUIData);
        UpdateTileSize(DailyUIData);
    }

    private void UpdateTileSize(CatagoryUIData UIData) => UIData.Tile.sizeDelta =
        new Vector2(UIData.DefaultSizeDelta.x, UIData.DefaultSizeDelta.y + (CatagoryOffset * UIData.Count));

    private Catagory InitializeCatagory(CatagoryData myCatagoryData, CatagoryUIData UIData) {
        Catagory newCatagory;
        if (UIData.Count == 0)
            newCatagory = UIData.Original;
        else {
            newCatagory = Instantiate(original: UIData.Original, parent: UIData.Parent.transform) as Catagory;
            newCatagory.transform.localPosition = new Vector3(UIData.StartPos.x, UIData.StartPos.y - (CatagoryOffset * UIData.Count), UIData.StartPos.z);
        }
        newCatagory.Construct();
        newCatagory.UpdateData(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }

    public List<CatagoryData> GetData() {
        List<CatagoryData> catagoryDatas = new List<CatagoryData>();
        foreach (Catagory catagory in AllCatagories)
            catagoryDatas.Add(catagory.GetData());
        return catagoryDatas;
    }
}

public class CatagoryUIData {
    public Catagory Original = null;
    public GameObject Parent = null;
    public Vector3 StartPos = null;
    public Vector2 DefaultSizeDelta = null;
    public RectTransform Tile = null;
    public int Count = 0;

    public CatagoryUIData(Catagory original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>()
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }
}

