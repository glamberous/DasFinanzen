using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSubCatagoryView : MonoBehaviour
{
    private void OnMouseDown() {
        Messenger<int>.Broadcast(AppEvent.SUB_VIEW_TOGGLE, Managers.Catagory.SelectedCatagory.ID);
    }
}
