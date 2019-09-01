using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddExpenseToggle : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private bool TriggerSave = false;

    public void OnPointerClick(PointerEventData eventData) => Messenger<bool>.Broadcast(AppEvent.ADD_EXPENSE_TOGGLE, TriggerSave);
}
