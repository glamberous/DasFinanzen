using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Catagory : MonoBehaviour {
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private Image ColorPatchImage;
    public List<ExpenseData> ExpenseDatas = new List<ExpenseData>();

    private bool Reoccurring;
    public int CatagoryID { get; private set; }
    private string colorCode;
    private string ColorCode {
        get => colorCode;
        set {
            Color newColor = ColorConverter.HexToColor(value);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
            colorCode = value;
        }
    }

    public void Construct() {
        ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public ExpenseData GetData() {
        ExpenseData data = new ExpenseData();
        return data;
    }

    public void UpdateData(CatagoryData data) {
        CatagoryID = data.CatagoryID;
        Reoccurring = data.Reoccurring;
        NameTextMesh.text = data.NameText;
        ColorCode = data.ColorCode;
        UpdateExpenseTotalText();
    }

    public void UpdateExpenseTotalText() => TotalTextMesh.text = GetExpensesTotal().ToString("C");

    public Decimal GetExpensesTotal() {
        Decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseDatas)
            total += expense.Amount;
        return total;
    }
}

