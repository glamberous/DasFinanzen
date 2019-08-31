using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CatagoryClick : Selectable {
    public void OnPointerDown(PointerEventData eventData) => Messenger<int>.Broadcast(AppEvent.SUB_VIEW_TOGGLE, gameObject.GetComponent<Catagory>().ID);
}
