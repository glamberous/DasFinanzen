using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Catagory : MonoBehaviour {
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private Image ColorPatchImage;

    private bool Reoccurring;
    private int CatagoryID;
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

    public CatagoryData GetData() {
        CatagoryData data = new CatagoryData();
        data.Reoccurring = Reoccurring;
        data.NameText = NameTextMesh.text;
        data.ColorCode = ColorCode;
        data.CatagoryID = CatagoryID;
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
        /*
        foreach (ExpenseData expense in Expenses)
            total += expense.Amount;
        */
        return total;
    }
}

