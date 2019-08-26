using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }

    // Catagories for other classes to reference.
    [System.NonSerialized] public List<Catagory> Catagories = new List<Catagory>();

    private const float CatagoryOffset = 30.0f;

    // Initialization Variables
    [SerializeField] private CatagoryData[] CatagoryDatas = null;
    [SerializeField] private Catagory DailyOriginal = null;
    [SerializeField] private Catagory MonthlyOriginal = null;

    public void Startup() {
        Debug.Log("Catagory manager starting...");

        InitializeCatagories();
        Managers.Data.LoadGameState();

        status = ManagerStatus.Started;
    }

    private void InitializeCatagories() {
        CatagoryUIData DailyUIData = new CatagoryUIData(DailyOriginal);
        CatagoryUIData MonthlyUIData = new CatagoryUIData(MonthlyOriginal);
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
        newCatagory.Construct(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }

    private void UpdateTileSize(CatagoryUIData UIData) => UIData.Tile.sizeDelta =
        new Vector2(UIData.DefaultSizeDelta.x, UIData.DefaultSizeDelta.y + (CatagoryOffset * (UIData.Count - 1)));

    public void LoadData(List<ExpenseData> expenseDatas) {
        ILookup<int, ExpenseData> sortedExpenseDatas = expenseDatas.ToLookup(data => data.CatagoryID);
        foreach (Catagory catagory in Catagories)
            catagory.LoadExpenses(sortedExpenseDatas[catagory.CatagoryID].ToList<ExpenseData>() ?? new List<ExpenseData>());
    }

    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatas = new List<ExpenseData>();
        foreach (Catagory catagory in Catagories)
            expenseDatas.AddRange(catagory.GetExpenseDatas());
        return expenseDatas;
    }
}

internal class CatagoryUIData {
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

