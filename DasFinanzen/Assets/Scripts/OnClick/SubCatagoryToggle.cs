using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubCatagoryToggle : Selectable, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData) {
        Messenger<int>.Broadcast(AppEvent.SUB_VIEW_TOGGLE, Managers.Catagory.SelectedCatagory.ID);
    }
}
