using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IDTracker {
    public static int CreateNew(IDType type) {
        switch(type) {
            case IDType.CATAGORY:   return Managers.Data.FileData.IDTrackerModel.CatagoryID + 1;
            case IDType.EXPENSE:    return Managers.Data.FileData.IDTrackerModel.ExpenseID + 1;
            default:                return -1;
        }
    }

    public static bool IsNew(IDType type, int id) {
        switch(type) {
            case IDType.CATAGORY:   return Managers.Data.FileData.IDTrackerModel.CatagoryID < id;
            case IDType.EXPENSE:    return Managers.Data.FileData.IDTrackerModel.ExpenseID < id;
            default:                return false;
        }
    }

    public static void SaveID(IDType type, int id) {
        switch (type) {
            case IDType.CATAGORY:   Managers.Data.FileData.IDTrackerModel.CatagoryID = id; break;
            case IDType.EXPENSE:    Managers.Data.FileData.IDTrackerModel.ExpenseID = id; break;
            default:                return;
        }
    }
}
