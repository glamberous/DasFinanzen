using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBarElement : MonoBehaviour {
        [HideInInspector]public int CatagoryID;

        private Image ColorBarImage = null;
        private RectTransform BarRect = null;

        public void Awake() {
            BarRect = gameObject.GetComponent<RectTransform>();
            ColorBarImage = gameObject.GetComponent<Image>();
        }

        public void UpdateView(CatagoryModel Model, float width) {
            CatagoryID = Model.CatagoryID;
            ColorBarImage.color = ColorConverter.HexToColor(Model.ColorCode);
            BarRect.sizeDelta = new Vector2(width, BarRect.sizeDelta.y);
        }
    }
}

