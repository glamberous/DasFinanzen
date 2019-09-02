
using UnityEngine;

public class TileUIData {
    public GameObject Original;
    public GameObject Parent;
    public Vector3 StartPos;
    public Vector2 DefaultSizeDelta;
    public RectTransform Tile;
    public int Count = 0;

    public TileUIData(GameObject original) {
        Original = original;
        StartPos = original.transform.localPosition;
        Parent = original.transform.parent.gameObject;
        Tile = Parent.GetComponent<RectTransform>();
        DefaultSizeDelta = Tile.sizeDelta;
        Count = 0;
    }

    public void UpdateTileSize() => Tile.sizeDelta =
        new Vector2(DefaultSizeDelta.x, DefaultSizeDelta.y + (Constants.CatagoryOffset * (Count - 1)));
}