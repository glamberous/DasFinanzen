using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    [SerializeField] private ColorBar OriginalColorBar = null;
    private ColorBar[] ColorBars = null;
    private decimal ExpensesGoal = 700.00m;

    public void Startup() {
        Debug.Log("ColorBar manager starting...");
        InitializeColorBar();
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);
        UpdateColorBar();
        status = ManagerStatus.Started;
    }

    public void LoadData(decimal goal) => ExpensesGoal = goal;
    public decimal GetData() => ExpensesGoal;

    public void InitializeColorBar() {
        ColorBars = new ColorBar[Managers.Catagory.Catagories.Count];
        int count = 0;
        foreach (KeyValuePair<int, Catagory> catagory in Managers.Catagory.Catagories) {
            ColorBar newBar;
            if (count == 0)
                newBar = OriginalColorBar;
            else 
                newBar = Instantiate(original: ColorBars[count-1], parent: ColorBars[count-1].transform.parent.gameObject.transform) as ColorBar;
            newBar.Construct(catagory.Value.ID, catagory.Value.ColorCode);
            ColorBars[count++] = newBar;
        }
    }

    private void UpdateColorBar() {
        float width = 0.00f;
        foreach (ColorBar colorBar in ColorBars) {
            width += ((float)(Managers.Catagory.Catagories[colorBar.ID].ExpensesTotal / ExpensesGoal) * Screen.width) + width;
            //decimal testExpenseTotal = Managers.Catagory.Catagories[colorBar.ID].ExpensesTotal;
            // ^ This keeps returning 0 even for my test Catagory.
            colorBar.Width = width;
        }
    }

    private void OnExpensesUpdated() {
        UpdateColorBar();
    }
}
