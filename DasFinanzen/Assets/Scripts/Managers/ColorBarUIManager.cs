using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarUIManager : MonoBehaviour, ManagerInterface {
    public ManagerStatus status { get; private set; }

    [SerializeField] private ColorBar OriginalColorBar = null;
    private Dictionary<int, ColorBar> ColorBarDict = new Dictionary<int, ColorBar>();

    public void Startup() {
        Debug.Log("ColorBar manager starting...");

        InitializeColorBar();
        Messenger.AddListener(AppEvent.EXPENSES_UPDATED, UpdateColorBar);

        status = ManagerStatus.Started;
    }

    public void InitializeColorBar() {
        int count = 0;
        foreach (KeyValuePair<int, CatagoryData> catagory in Managers.Data.CatagoryDataDict) {
            ColorBar newBar;
            if (count++ == 0)
                newBar = OriginalColorBar;
            else 
                newBar = Instantiate(original: OriginalColorBar, parent: OriginalColorBar.transform.parent.gameObject.transform) as ColorBar;
            newBar.Initialize(catagory.Value);
            ColorBarDict[catagory.Value.ID] = newBar;
        }
        UpdateColorBar();
    }

    public void UpdateColorBar() {
        float tempFloat = 0.00f;
        foreach (KeyValuePair<int, ColorBar> colorBar in ColorBarDict) {
            colorBar.Value.transform.localPosition = new Vector3(tempFloat, 0, 0);
            colorBar.Value.SetWidth(Managers.Data.GetWidthBasedOffPercentOfScreenWidth(colorBar.Key));
            tempFloat += colorBar.Value.GetWidth();
        }
    }

    private void OnDestroy() => Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, UpdateColorBar);
}
