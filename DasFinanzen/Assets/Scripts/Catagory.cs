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
    private bool Reoccurring;
    public int ID { get; private set; }

    private string nameText;
    [HideInInspector] public string NameText {
        get => nameText;
        private set {
            nameText = value;
            NameTextMesh.text = value;
        }
    }

    private string colorCode;
    [HideInInspector] public string ColorCode {
        get => colorCode;
        private set {
            colorCode = value;
            Color newColor = ColorConverter.HexToColor(value);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
        }
    }

    private decimal expensesTotal;
    public decimal ExpensesTotal {
        get => expensesTotal;
        private set {
            expensesTotal = value;
            TotalTextMesh.text = value.ToString("C");
        }
    }

    public void Construct(CatagoryData data) {
        ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        SetCatagoryData(data);
    }

    private void SetCatagoryData(CatagoryData data) {
        ID = data.ID;
        Reoccurring = data.Reoccurring;
        NameText = data.NameText;
        ColorCode = data.ColorCode;
        UpdateExpensesTotal(); //Ensures default reported Expense Total is $0.00
    }

    public void UpdateExpensesTotal() => ExpensesTotal = Managers.Expense.GetExpensesTotal(ID);
}

