using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBar : MonoBehaviour {
    [SerializeField] private GameObject OriginalColorBarObject = null;

    private void Awake() {
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);
    }

    private void OnExpensesUpdated() {
        List<ColorBarCatagoryData> colorBarCatagoryDatas = new List<ColorBarCatagoryData>();
        //foreach(Catagory catagory in Managers.Catagory.Catagories) 
        
    }
}

internal class ColorBarCatagoryData {
    decimal TotalExpenses = 0.00m;
    string ColorCode = "FFFFFF";
    float RightRect = 0.00f;
    
    public ColorBarCatagoryData(Catagory catagory) {
        TotalExpenses = catagory.GetExpensesTotal();
        ColorCode = catagory.ColorCode;
    }
}