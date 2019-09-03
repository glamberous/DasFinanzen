using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI AddExpenseTextProxy = null;
    [SerializeField] private TMP_InputField AddExpenseInputField = null;

    public void AdjustCurrencyInput(string input) {
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
}
