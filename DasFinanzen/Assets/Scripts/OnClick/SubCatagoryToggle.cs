using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubCatagoryToggle : MonoBehaviour {
    private void OnMouseDown() => Messenger<int>.Broadcast(AppEvent.SUB_VIEW_TOGGLE, Managers.Catagory.SelectedCatagory.ID);
}
