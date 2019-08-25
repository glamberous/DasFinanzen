using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    [System.NonSerialized] public List<Catagory> Catagories = new List<Catagory>();
    public CatagoryData[] CatagoryDatas = null;

    private const float CatagoryOffset = 30.0f;

    [SerializeField] private Catagory DailyOriginal = null;
    private CatagoryUIData DailyUIData = null;

    [SerializeField] private Catagory MonthlyOriginal = null;
    private CatagoryUIData MonthlyUIData = null;

    public void Startup() {
        Debug.Log("Catagory manager starting...");

        DailyUIData = new CatagoryUIData(DailyOriginal);
        MonthlyUIData = new CatagoryUIData(MonthlyOriginal);
        InitializeCatagories();
        //Managers.Data.LoadGameState();
        Managers.Data.SaveGameState();


        //Get and Load all catagory data here?
        //filename = Path.Combine(Application.persistentDataPath, "game.dat");

        status = ManagerStatus.Started;
    }

    private void InitializeCatagories() {
        foreach (CatagoryData data in CatagoryDatas)
            if (data.Reoccurring)
                Catagories.Add(InitializeCatagory(data, MonthlyUIData));
            else
                Catagories.Add(InitializeCatagory(data, DailyUIData));
        UpdateTileSize(MonthlyUIData);
        UpdateTileSize(DailyUIData);
    }

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

    private void UpdateTileSize(CatagoryUIData UIData) => UIData.Tile.sizeDelta =
        new Vector2(UIData.DefaultSizeDelta.x, UIData.DefaultSizeDelta.y + (CatagoryOffset * (UIData.Count - 1)));

    public void UpdateData(List<ExpenseData> expenseDatas) {
        foreach (ExpenseData expenseData in expenseDatas)
            foreach (Catagory catagory in Catagories)
                if (expenseData.CatagoryID == catagory.CatagoryID) {
                    catagory.ExpenseDatas.Add(expenseData);
                    break;
                }  
    }

    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatas = new List<ExpenseData>();
        foreach (Catagory catagory in Catagories)
            expenseDatas.Add(catagory.GetData());
        return expenseDatas;
    }
}

public class CatagoryUIData {
    public Catagory Original;
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;
    public int Count = 0;

    public CatagoryUIData(Catagory original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }
}

