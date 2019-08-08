using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Catagory : MonoBehaviour {
    [SerializeField] private CatagoryData defaultData;

    private TextMeshProUGUI NameTextMesh;
    private TextMeshProUGUI TotalTextMesh;
    private Image ColorPatchImage;
    private bool Reoccurring;
    private string colorCode;
    private string ColorCode {
        get => colorCode;
        set {
            Color newColor = GetColor32FromColorHex(value);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
            colorCode = value;
        }
    }

    private int GetIntFromChar(char myChar) {
        myChar = char.ToLower(myChar);
        if (myChar >= '0' && myChar <= '9') 
            return (myChar - '0');   
        if (myChar >= 'a' && myChar <= 'f') 
            return (myChar - 'a') + 10;
        return -1;
    }

    private int ConvertHexStringToInt(string hexString) {
        int power = 1;
        int total = GetIntFromChar(hexString[hexString.Length - 1]);
        for (int indexer = hexString.Length - 2; indexer >= 0; indexer--, power++) {
            total += GetIntFromChar(hexString[indexer]) * (int)Mathf.Pow(16, power);
        }
        return total;
    }

    private Color32 GetColor32FromColorHex(string hexCode) {
        Color32 ColorFromHex = new Color32(
            (byte)ConvertHexStringToInt(new string(new char[2] { hexCode[0], hexCode[1] })),
            (byte)ConvertHexStringToInt(new string(new char[2] { hexCode[2], hexCode[3] })),
            (byte)ConvertHexStringToInt(new string(new char[2] { hexCode[4], hexCode[5] })),
            (byte)255
        );
        return ColorFromHex;
    }

    public void LoadData(CatagoryData data) {
        Reoccurring = data.Reoccurring;
        NameTextMesh.text = data.NameText;
        ColorCode = data.ColorCode;
    }

    public void Start() {
        Debug.Log("I started.");
        ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        TotalTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        NameTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        LoadData(defaultData);
    }
}
