using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ColorBarElement : MonoBehaviour {
        [HideInInspector] public int CatagoryID { get; private set; }
        public void SetID(int catagoryID) => CatagoryID = catagoryID;

        private Image ColorBarImage = null;
        private RectTransform BarRect = null;

        public void Awake() {
            BarRect = gameObject.GetComponent<RectTransform>();
            ColorBarImage = gameObject.GetComponent<Image>();
        }

        public void UpdateView(CatagoryModel Model, float width) {
            // Fixes a float inaccuracy bug where a sliver of color for catagories that have no expenses was showing.
            width = width >= 1 ? width : 0;
            BarRect.sizeDelta = new Vector2(width, BarRect.sizeDelta.y);

            ColorBarImage.color = ColorConverter.HexToColor(Model.ColorCode);
        }
    }
}

