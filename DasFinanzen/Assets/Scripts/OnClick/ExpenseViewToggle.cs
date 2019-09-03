﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]

public class ExpenseViewToggle : MonoBehaviour {
    private void OnMouseDown() {
        Catagory catagory = gameObject.GetComponent<Catagory>();
        if (catagory != null)
            Managers.Catagory.CurrentID = catagory.ID;
        Messenger.Broadcast(AppEvent.EXPENSE_VIEW_TOGGLE);
    }
}