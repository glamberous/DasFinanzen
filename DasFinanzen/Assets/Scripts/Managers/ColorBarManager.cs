using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }
    [SerializeField] private ColorBar OriginalColorBar = null;
    private ColorBar PrevColorBar = null;
    private decimal TotalRemaining = 700.00m;

    public void Startup() {
        Debug.Log("ColorBar manager starting...");
        InitializeColorBar();
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);

        status = ManagerStatus.Started;
    }

    public void InitializeColorBar() {
        int count = 0;
        foreach (Catagory catagory in Managers.Catagory.Catagories) {
            ColorBar newBar;
            if (count == 0)
                newBar = OriginalColorBar;
            else 
                newBar = Instantiate(original: PrevColorBar, parent: PrevColorBar.transform.parent.gameObject.transform) as ColorBar;
            newBar.CatagoryID = catagory.CatagoryID;
            newBar.ColorCode = catagory.ColorCode;
            newBar.Width = (float)((catagory.GetExpensesTotal() / TotalRemaining) * Screen.width);
            PrevColorBar = newBar;
            count++;
        }
    }

    public void OnExpensesUpdated() {

    }
}
