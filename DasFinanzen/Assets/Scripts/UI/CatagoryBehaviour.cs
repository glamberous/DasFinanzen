using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatagoryBehaviour : MonoBehaviour {
    [HideInInspector] public Catagory Instance = null;
    private void Awake() => Instance = new Catagory(gameObject);
}

public class Catagory {
    private GameObject SelfObject;
    public TextMeshProUGUI NameTextMesh { get; private set; }
    public TextMeshProUGUI TotalTextMesh { get; private set; }
    public TextMeshProUGUI CurrencySymbol { get; private set; }
    public Image ColorPatchImage { get; private set; }
    public CatagoryData Data = null;

    public Catagory(GameObject gameObject) => SelfObject = gameObject;

    public void Initialize(CatagoryData data) {
        Construct();
        SetCatagoryData(data);
        UpdateExpensesTotal();
    }

    private void Construct() {
        ColorPatchImage = Managers.Error.TryGetImage(SelfObject.transform.GetChild(0)?.gameObject.GetComponent<Image>());
        NameTextMesh = Managers.Error.TryGetTextMeshProUGUI(SelfObject.transform.GetChild(1)?.gameObject.GetComponent<TextMeshProUGUI>());
        TotalTextMesh = Managers.Error.TryGetTextMeshProUGUI(SelfObject.transform.GetChild(2)?.gameObject.GetComponent<TextMeshProUGUI>());
        CurrencySymbol = Managers.Error.TryGetTextMeshProUGUI(TotalTextMesh.gameObject.transform.GetChild(0)?.gameObject.GetComponent<TextMeshProUGUI>());
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

