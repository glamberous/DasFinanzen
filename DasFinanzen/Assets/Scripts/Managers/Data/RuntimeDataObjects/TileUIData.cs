
using UnityEngine;

public class TileUIData {
    public GameObject Original;
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;

    public TileUIData(GameObject original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
    }

    public void UpdateTileSize(int count) => 
        Tile.sizeDelta = new Vector2(DefaultSizeDelta.x, DefaultSizeDelta.y + (Constants.CatagoryOffset * (count - 1)));
}