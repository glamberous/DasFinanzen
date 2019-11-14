
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    public class CatagoryElement : Button {
        [HideInInspector] public int CatagoryID { get; private set; } = -1;
        public void SetCatagoryID(int id) => CatagoryID = id;

        // Inherited from Generic_Button
        public override void OnMouseUp() => Controller.TriggerCommand(CommandID, CatagoryID.ToString());

        private Image ColorPatchImage;
        private TextMeshProUGUI NameTextMesh;
        private TextMeshProUGUI TotalTextMesh;

        public void Awake() {
            ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void UpdateView(CatagoryModel model, decimal catagoryTotal) {
            NameTextMesh.text = model.NameText;
            SetColor(model.ColorCode);
            TotalTextMesh.text = "$" + catagoryTotal.ToString();
        }

        private void SetColor(string colorCode) {
            Color newColor = ColorConverter.HexToColor(colorCode);
            NameTextMesh.color = newColor;
            TotalTextMesh.color = newColor;
            ColorPatchImage.color = newColor;
        }
    }
}


