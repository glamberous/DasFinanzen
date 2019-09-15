using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatagoryMono : MonoBehaviour {
    [HideInInspector] public Catagory Instance = null;
    private void Awake() => Instance = new Catagory(gameObject);
}

public class Catagory {
    private GameObject SelfMono;
    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private TextMeshProUGUI CurrencySymbol;
    private Image ColorPatchImage;
    public CatagoryData Data = null;

    public Catagory(GameObject gameObject) => SelfMono = gameObject;

    public void Initialize(CatagoryData data) {
        Construct();
        SetCatagoryData(data);
        UpdateExpensesTotal();
    }

    public void Construct() {
        ColorPatchImage = SelfMono.transform.GetChild(0).gameObject.GetComponent<Image>();
        NameTextMesh = SelfMono.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TotalTextMesh = SelfMono.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
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

