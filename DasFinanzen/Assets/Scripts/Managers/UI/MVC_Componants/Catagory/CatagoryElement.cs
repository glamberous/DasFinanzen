using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class CatagoryElement : MonoBehaviour {
    private GameObject SelfObject;
    public TextMeshProUGUI NameTextMesh { get; private set; }
    public TextMeshProUGUI TotalTextMesh { get; private set; }
    public TextMeshProUGUI CurrencySymbol { get; private set; }
    public Image ColorPatchImage { get; private set; }

    public CatagoryElement(GameObject gameObject) => SelfObject = gameObject;

    public void Initialize(CatagoryData data) {
        Construct();
        SetCatagoryData(data);
        //TODO UpdateExpensesTotal();
    }

    private void Construct() {
        ColorPatchImage = SelfObject.transform.GetChild(0)?.gameObject.GetComponent<Image>();
        NameTextMesh = SelfObject.transform.GetChild(1)?.gameObject.GetComponent<TextMeshProUGUI>();
        TotalTextMesh = SelfObject.transform.GetChild(2)?.gameObject.GetComponent<TextMeshProUGUI>();
        CurrencySymbol = TotalTextMesh.gameObject.transform.GetChild(0)?.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetCatagoryData(CatagoryData data) {
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

    //TODO public void UpdateExpensesTotal() => TotalTextMesh.text = Managers.Data.GetExpensesTotal(Data.ID).ToString();
}

