using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class Catagory : MonoBehaviour {
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private Image ColorPatchImage;
    private List<ExpenseData> ExpenseDatas = new List<ExpenseData>();
    private bool Reoccurring;
    public int ID { get; private set; }

    private string colorCode;
    [HideInInspector]
    public string ColorCode {
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
    [HideInInspector] public decimal ExpensesTotal {
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
        NameTextMesh.text = data.NameText;
        ColorCode = data.ColorCode;
        UpdateExpensesTotal(); //Ensures default reported Expense Total is $0.00
    }

    public List<ExpenseData> GetExpenseDatas() {
        return ExpenseDatas;
    }

    public void AddExpense(ExpenseData data) {
        ExpenseDatas.Add(data);
        UpdateExpensesTotal();
        Messenger.Broadcast(AppEvent.EXPENSES_UPDATED);
    }

    public void LoadExpenses(List<ExpenseData> expenseDatas) {
        ExpenseDatas = expenseDatas;
        UpdateExpensesTotal();
    }

    private void UpdateExpensesTotal() => ExpensesTotal = GetExpensesTotal();
    private decimal GetExpensesTotal() {
        decimal total = 0.00m;
        foreach (ExpenseData expense in ExpenseDatas)
            total += expense.Amount;
        return total;
    }
}

