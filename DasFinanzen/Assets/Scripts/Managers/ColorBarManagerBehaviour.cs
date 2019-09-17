﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarManagerBehaviour : MonoBehaviour {
    [SerializeField] private ColorBar OriginalColorBar = null;

    public ColorBarManager Manager { get; private set; }
    private void Awake() => Manager = new ColorBarManager(OriginalColorBar);
}

public class ColorBarManager : ManagerInterface {
    private Dictionary<int, ColorBar> ColorBarDict = new Dictionary<int, ColorBar>();
    private ColorBar OriginalColorBar = null;

    public ColorBarManager(ColorBar originalColorBar) {
        OriginalColorBar = originalColorBar;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("ColorBar manager starting...");

        InitializeColorBar();
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, UpdateColorBar);
        Messenger.AddListener(AppEvent.GOAL_UPDATED, UpdateColorBar);

        status = ManagerStatus.Started;
    }

    private void InitializeColorBar() {
        int count = 0;
        foreach (KeyValuePair<int, CatagoryData> catagory in Managers.Data.CatagoryDataDict) {
            ColorBar newBar;
            if (count++ == 0)
                newBar = OriginalColorBar;
            else 
                newBar = GameObject.Instantiate(original: OriginalColorBar, parent: OriginalColorBar.transform.parent.gameObject.transform) as ColorBar;
            newBar.Initialize(catagory.Value);
            ColorBarDict[catagory.Value.ID] = newBar;
        }
        UpdateColorBar();
    }

    private void UpdateColorBar() {
        float tempFloat = 0.00f;
        foreach (KeyValuePair<int, ColorBar> colorBar in ColorBarDict) {
            colorBar.Value.transform.localPosition = new Vector3(tempFloat, 0, 0);
            colorBar.Value.SetWidth(GetWidthBasedOffPercentOfScreenWidth(colorBar.Key));
            tempFloat += colorBar.Value.GetWidth();
        }
    }

    // TODO - Bug here with needing to reference the Canvas Width, not screen width.
    private float GetWidthBasedOffPercentOfScreenWidth(int ID) => ((float)Managers.Data.GetExpensesTotal(ID) / (float)Managers.Data.BudgetGoal) * 337.5f;    
}