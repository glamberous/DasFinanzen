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

    private decimal Amount {
        get => Amount;
        set {
            Amount = value;
            ExpenseTextMesh.text = value.ToString();
        }
    }

    private string NameText {
        get => NameText;
        set {
            NameText = value;
            NameTextMesh.text = value;
        }
    }

    private int Date {
        get => Date;
        set {
            Date = value;
            DateTextMesh.text = value.ToString();
        }
    }

    private string ColorCode {
        get => ColorCode;
        set {
            ColorCode = value;
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
