using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Catagory : MonoBehaviour {
    [SerializeField] private CatagoryData defaultData;
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private Image ColorPatchImage;
    private bool Reoccurring;
    public ExpenseData[] ExpenseData;
    private string colorCode;
    private string ColorCode {
        get => colorCode;
        set {
            Color newColor = HexToColor(value);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
            colorCode = value;
        }
    }

    public void Construct(CatagoryData data = defaultData) {
        ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        TotalTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        NameTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        LoadData(data);
    }

    public void LoadData(CatagoryData data) {
        Reoccurring = data.Reoccurring;
        NameTextMesh.text = data.NameText;
        ColorCode = data.ColorCode;
        ExpenseData = data.ExpenseData;
        TotalTextMesh.text = GetExpensesTotal().ToString("C");
    }
    public void UpdateExpenseTotal() => TotalTextMesh.text = GetExpensesTotal().ToString("C");

    public Decimal GetExpensesTotal() {
        Decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseData)
            total += expense.Amount;
        return total;
    }

    public static Color HexToColor(string hex) {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8) {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}

