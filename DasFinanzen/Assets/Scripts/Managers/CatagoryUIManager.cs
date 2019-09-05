﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CatagoryUIManager : MonoBehaviour, ManagerInterface {
    // Variables for other classes to reference.
    [HideInInspector] public Dictionary<int, Catagory> CatagoryUIs = new Dictionary<int, Catagory>();

    // Initialization Variables
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
        foreach (KeyValuePair<int, CatagoryData> data in Managers.Data.CatagoryDataDict)
            if (data.Value.Reoccurring)
                CatagoryUIs.Add(data.Value.ID, InitializeCatagory(data.Value, MonthlyUIData));
            else
                CatagoryUIs.Add(data.Value.ID, InitializeCatagory(data.Value, DailyUIData));
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
        newCatagory.Initialize(myCatagoryData);
        UIData.Count++;
        return newCatagory;
    }

    private void OnExpensesUpdated() {
        foreach (KeyValuePair<int, Catagory> catagory in CatagoryUIs)
            catagory.Value.UpdateExpensesTotal();
    }

    private void OnDestroy() => Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, OnExpensesUpdated);
}