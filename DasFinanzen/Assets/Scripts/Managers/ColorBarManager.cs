using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    [SerializeField] private ColorBar OriginalColorBar = null;
    private ColorBar[] ColorBars = null;
    private decimal ExpensesGoal = 700.00m; //TODO - Break this into it's own class

    public void Startup() {
        Debug.Log("ColorBar manager starting...");
        InitializeColorBar();
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);

        status = ManagerStatus.Started;
    }

    public void LoadData(decimal goal) => ExpensesGoal = goal;
    public decimal GetData() => ExpensesGoal;

    #region Initialization
    public void InitializeColorBar() {
        ColorBars = new ColorBar[Managers.Catagory.Catagories.Count];
        int count = 0;
        foreach (KeyValuePair<int, Catagory> catagory in Managers.Catagory.Catagories) {
            ColorBar newBar;
            if (count == 0)
                newBar = OriginalColorBar;
            else 
                newBar = Instantiate(original: OriginalColorBar, parent: OriginalColorBar.transform.parent.gameObject.transform) as ColorBar;
            newBar.Construct(catagory.Value.ID, catagory.Value.ColorCode);
            ColorBars[count++] = newBar;
        }
    }
    #endregion

    private void UpdateColorBar() {
        float tempFloat = 0.00f;
        foreach (ColorBar colorBar in ColorBars) {
            colorBar.transform.localPosition = new Vector3(tempFloat - 310.0f, 0, 0); // Needs - 310.0f here cause of Parent Issues that I can't figure out. 310 is added during runtime for some reason.
            colorBar.Width = (((float)Managers.Catagory.Catagories[colorBar.ID].ExpensesTotal / (float)ExpensesGoal) * Screen.width);
            tempFloat += colorBar.Width;
        }
    }

    private void OnExpensesUpdated() {
        UpdateColorBar();
    }
}
