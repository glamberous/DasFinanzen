using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Catagory : MonoBehaviour {
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private TextMeshProUGUI CurrencySymbol;
    private Image ColorPatchImage;
    [HideInInspector] public CatagoryData Data = null;

    public void Initialize(CatagoryData data) {
        Construct();
        SetCatagoryData(data);
        UpdateExpensesTotal();
    }

    public void Construct() {
        ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        CurrencySymbol = TotalTextMesh.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetCatagoryData(CatagoryData data) {
        Data = data;
        NameTextMesh.text = data.NameText;
        SetColor(data.ColorCode);
    }

    private void SetColor(string colorCode) {
        Color newColor = ColorConverter.HexToColor(colorCode);
        NameTextMesh.color = newColor;
        TotalTextMesh.color = newColor;
        ColorPatchImage.color = newColor;
        CurrencySymbol.color = newColor;
    }

    public void UpdateExpensesTotal() => TotalTextMesh.text = Managers.Data.GetExpensesTotal(Data.ID).ToString();
}

