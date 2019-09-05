using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expense : MonoBehaviour {
    private TextMeshProUGUI DateTextMesh = null;
    private TextMeshProUGUI NameTextMesh = null;
    private TextMeshProUGUI ExpenseTextMesh = null;
    [HideInInspector] public ExpenseData Data = null;

    public void Construct(ExpenseData data) {
        DateTextMesh = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        ExpenseTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        SetExpenseData(data);
    }

    public void SetColor(string colorCode) {
        Color newColor = ColorConverter.HexToColor(colorCode);
        NameTextMesh.color = newColor;
        DateTextMesh.color = newColor;
        ExpenseTextMesh.color = newColor;
    }

    public void SetDate(long date) {
        System.DateTimeOffset newDate = System.DateTimeOffset.FromUnixTimeSeconds(date);
        DateTextMesh.text = string.Format("{0}/{1}", newDate.Month.ToString(), newDate.Day.ToString());
    }

    public void SetExpenseData(ExpenseData data) {
        Data = data;
        ExpenseTextMesh.text = data.Amount.ToString("C");
        NameTextMesh.text = data.NameText;
        SetDate(data.EpochDate);
        SetColor(Managers.Data.CurrentCatagoryData.ColorCode);
    }
}
