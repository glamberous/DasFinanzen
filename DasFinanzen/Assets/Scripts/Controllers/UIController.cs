using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject SubCatagoryView = null;
    [SerializeField] private GameObject GraphView = null;

    private void Awake() {
        Messenger<int>.AddListener(AppEvent.SUB_VIEW_TOGGLE, OnSubViewToggled);
    }

    private void OnDestroy() {
        Messenger<int>.RemoveListener(AppEvent.SUB_VIEW_TOGGLE, OnSubViewToggled);
    }

    private void OnSubViewToggled(int catagoryID) {
        if (SubCatagoryView.activeInHierarchy) {
            Managers.Catagory.ExitSubCatagoryView();
            SubCatagoryView.SetActive(false);
        }
        else {
            Managers.Catagory.PrepareSubCatagoryView(catagoryID);
            SubCatagoryView.SetActive(true);
        }
    }


}
