using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI AddExpenseTextProxy = null;
    [SerializeField] private TMP_InputField AddExpenseInputField = null;
    [SerializeField] private TMP_InputField NameInputField = null;

    public void AmountOnValueChanged() {
        string input = AddExpenseInputField.text;
        if (input.StartsWith("0"))
            AddExpenseInputField.text = input.TrimStart('0');
        else {
            string tempString = input;
            tempString = tempString.PadLeft(3, '0');
            tempString = tempString.Insert(tempString.Length - 2, ".");
            if (tempString.Length > 6)
                tempString = tempString.Insert(tempString.Length - 6, ",");

            AddExpenseTextProxy.text = tempString;
        }
    }

    // Not Taking the value in here because I have a TextMeshPro Proxy in order to display the text properly in the Scene.
    // EditExpenseUI Manager handles this in EditExpensesUI.
    public void AmountSave() => Managers.EditExpenseUI.UpdateEditExpenseAmount();
    public void NameSave() => Managers.EditExpenseUI.UpdateEditExpenseName(NameInputField.text);

}
