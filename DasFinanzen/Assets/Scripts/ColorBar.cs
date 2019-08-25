using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBar : MonoBehaviour {
    [SerializeField] private GameObject OriginalColorBarObject = null;

    private void Awake() {
        Messenger.AddListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);
    }

    private void OnDestroy() {
        Messenger.RemoveListener(CatagoryEvent.EXPENSES_UPDATED, OnExpensesUpdated);
    }

    private void OnExpensesUpdated() {
        
    }
}