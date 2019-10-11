﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBarElement : MonoBehaviour {
        public int ID { get; private set; } = -1;
        private Image ColorBarImage = null;
        private RectTransform BarRect = null;

        public void Initialize(CatagoryData data) {
            Construct();
            SetColor(data.ColorCode);
            ID = data.ID;
        }

        public void Construct() {
            BarRect = gameObject.GetComponent<RectTransform>();
            ColorBarImage = gameObject.GetComponent<Image>();
        }

        public void SetColor(string colorCode) => ColorBarImage.color = ColorConverter.HexToColor(colorCode);
        public void SetWidth(float width) => BarRect.sizeDelta = new Vector2(width, BarRect.sizeDelta.y);
        public float GetWidth() => BarRect.sizeDelta.x;
    }
}

