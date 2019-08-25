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
    //public ExpenseData[] ExpenseData;
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
        TotalTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        NameTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public CatagoryData GetData() {
        CatagoryData data;
        data.Reoccurring = Reoccurring;
        data.NameText = NameTextMesh.text;
        data.ColorCode = ColorCode;
        data.Expenses = GetAllExpenseData();
        return data;
    }

    public void UpdateData(CatagoryData data) {
        Reoccurring = data.Reoccurring;
        NameTextMesh.text = data.NameText;
        ColorCode = data.ColorCode;
        ExpenseData = data.ExpenseData;
        UpdateExpenseTotalText();
    }

    public void UpdateExpenseTotalText() => TotalTextMesh.text = GetExpensesTotal().ToString("C");
    public Decimal GetExpensesTotal() {
        Decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseData)
            total += expense.Amount;
        return total;
    }
}

