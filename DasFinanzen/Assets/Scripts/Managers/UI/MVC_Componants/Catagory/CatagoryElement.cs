﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class CatagoryElement : MonoBehaviour {
        public int CatagoryID;

        private Image ColorPatchImage;
        private TextMeshProUGUI NameTextMesh;
        private TextMeshProUGUI TotalTextMesh;
        private TextMeshProUGUI CurrencySymbol;

        private void Awake() {
            ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            CurrencySymbol = TotalTextMesh.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void UpdateView(CatagoryModel model, decimal catagoryTotal) {
            NameTextMesh.text = model.NameText;
            SetColor(model.ColorCode);
            CatagoryID = model.CatagoryID;
            TotalTextMesh.text = catagoryTotal.ToString();
        }

        private void SetColor(string colorCode) {
            Color newColor = ColorConverter.HexToColor(colorCode);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
            CurrencySymbol.color = newColor;
        }
    }
}

