using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorBar : MonoBehaviour {
    private Image ColorBarImage = null;
    private RectTransform BarRect = null;
    private string colorCode = null;
    private float width = 0.00f;

    [HideInInspector]
    public int ID { get; private set; }

    [HideInInspector]
    public string ColorCode {
        get => colorCode;
        private set {
            colorCode = value;
            ColorBarImage.color = ColorConverter.HexToColor(value);
        }
    }
    [HideInInspector]
    public float Width {
        get => width;
        set {
            width = value;
            BarRect.sizeDelta = new Vector2(value, BarRect.sizeDelta.y);
        }
    }

    public void Construct(int id, string hexColor) {
        BarRect = gameObject.GetComponent<RectTransform>();
        ColorBarImage = gameObject.GetComponent<Image>();
        ID = id;
        ColorCode = hexColor;
    }

}