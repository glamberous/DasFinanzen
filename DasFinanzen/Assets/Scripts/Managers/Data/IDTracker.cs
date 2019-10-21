using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IIDTracker {
    int CreateNew(IDType type);
}

public class IDTracker {
    private IDTrackerModel Model = null;
    public IDTracker(IDTrackerModel model) => Model = model;

    public int CreateNew(IDType type) {
        switch(type) {
            case IDType.CATAGORY:   return Model.CatagoryID++;
            case IDType.EXPENSE:    return Model.ExpenseID++;
            default:                return -1;
        }
    }
}
