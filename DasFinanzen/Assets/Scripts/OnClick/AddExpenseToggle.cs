using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddExpenseToggle : MonoBehaviour, IPointerDownHandler {
    [SerializeField] private bool TriggerSave = false;

    public void OnPointerDown(PointerEventData eventData) => Messenger<bool>.Broadcast(AppEvent.ADD_EXPENSE_TOGGLE, TriggerSave);
}
