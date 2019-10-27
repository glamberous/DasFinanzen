
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI {
    [RequireComponent(typeof(BoxCollider2D))]
    public class CatagoryElement : MonoBehaviour, IControllerElement {
        [HideInInspector] public int CatagoryID { get; private set; } = -1;
        private IController Controller = null;
        public void SetController(IController controller) => Controller = controller;

        private int CommandID = -1;
        public void SetCommandID(int commandID) => CommandID = commandID;

        public void SetID(int id) => CatagoryID = id;
        public void OnMouseDown() => Controller.TriggerCommand(CommandID, CatagoryID.ToString());

        private Image ColorPatchImage;
        private TextMeshProUGUI NameTextMesh;
        private TextMeshProUGUI TotalTextMesh;
        private TextMeshProUGUI CurrencySymbol;

        public void Awake() {
            ColorPatchImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            NameTextMesh = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            TotalTextMesh = gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            CurrencySymbol = TotalTextMesh.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void UpdateView(CatagoryModel model, decimal catagoryTotal) {
            NameTextMesh.text = model.NameText;
            SetColor(model.ColorCode);
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


