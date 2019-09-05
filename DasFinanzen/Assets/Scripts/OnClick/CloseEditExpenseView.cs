using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]

public class CloseEditExpenseView : MonoBehaviour {
    [SerializeField] private bool TriggerSave = false;

    private void OnMouseDown() => Messenger<bool>.Broadcast(AppEvent.CLOSE_EDIT_EXPENSE_VIEW, TriggerSave);
}
