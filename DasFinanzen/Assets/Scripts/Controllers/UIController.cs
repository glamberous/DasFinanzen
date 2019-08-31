using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject SubCatagoryView = null;
    [SerializeField] private GameObject AddExpenseView = null;
    [SerializeField] private GameObject GraphView = null;

    private void Awake() {
        Messenger<int>.AddListener(AppEvent.SUB_VIEW_TOGGLE, OnSubViewToggled);
        Messenger<bool>.AddListener(AppEvent.ADD_EXPENSE_TOGGLE, OnAddExpenseToggled);

        AddExpenseView.SetActive(false);
        SubCatagoryView.SetActive(false);
    }

    private void OnDestroy() {
        Messenger<int>.RemoveListener(AppEvent.SUB_VIEW_TOGGLE, OnSubViewToggled);
        Messenger<bool>.RemoveListener(AppEvent.ADD_EXPENSE_TOGGLE, OnAddExpenseToggled);
    }

    //private void ActivateViewColliders(string )

    private void OnSubViewToggled(int catagoryID) {
        if (SubCatagoryView.activeInHierarchy) {
            SubCatagoryView.SetActive(false);
            Managers.Catagory.DeconstructSubCatagoryView();
        }
        else {
            Managers.Catagory.ConstructSubCatagoryView(catagoryID);
            SubCatagoryView.SetActive(true);
        }
    }

    private void OnAddExpenseToggled(bool triggerSave) {
        if (triggerSave) 
            // Trigger Save of new Expense
        if (AddExpenseView.activeInHierarchy) {
            AddExpenseView.SetActive(false);
            Managers.Catagory.DeconstructAddExpenseView();
        } else {
            Managers.Catagory.ConstructAddExpenseView();
            AddExpenseView.SetActive(true);
        }
    }


}
