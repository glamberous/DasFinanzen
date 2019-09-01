using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddExpenseToggle : MonoBehaviour {
    [SerializeField] private bool TriggerSave = false;

    private void OnMouseDown() => Messenger<bool>.Broadcast(AppEvent.ADD_EXPENSE_TOGGLE, TriggerSave);
}
