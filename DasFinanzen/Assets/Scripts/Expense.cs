using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expense : MonoBehaviour {
    [HideInInspector] public int ID { get; private set; }
    private TextMeshProUGUI DateTextMesh = null;
    private TextMeshProUGUI NameTextMesh = null;
    private TextMeshProUGUI ExpenseTextMesh = null;

    private decimal amount;
    private decimal Amount {
        get => amount;
        set {
            amount = value;
            ExpenseTextMesh.text = value.ToString("C");
        }
    }

    private string nameText;
    private string NameText {
        get => nameText;
        set {
            nameText = value;
            NameTextMesh.text = value;
        }
    }

    private int date;
    private int Date {
        get => date;
        set {
            date = value;
            DateTextMesh.text = value.ToString();
        }
    }

    private string colorCode;
    private string ColorCode {
        get => colorCode;
        set {
            colorCode = value;
            Color newColor = ColorConverter.HexToColor(value);
            NameTextMesh.color = newColor;
            DateTextMesh.color = newColor;
            ExpenseTextMesh.color = newColor;
        }
    }

    public void Construct(ExpenseData data) {
        DateTextMesh = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        ExpenseTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        SetExpenseData(data);
    }

    public void SetExpenseData(ExpenseData data) {
        ColorCode = Managers.Catagory.SelectedCatagory.ColorCode;
        Amount = data.Amount;
        NameText = data.NameText;
        Date = data.EpochDate;
        ID = data.ID;
    }

    public ExpenseData GetExpenseData() {
        ExpenseData newData = new ExpenseData();
        newData.Amount = Amount;
        newData.NameText = NameText;
        newData.EpochDate = Date;
        newData.ID = ID;
        return newData;
    }
}
