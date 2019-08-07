using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catagory : MonoBehaviour {
    [SerializeField] private CatagoryData defaultData = null;

    private bool Reoccurring;
    private string Name;
    private string ColorCode;

    public void LoadData(CatagoryData data) {
        Reoccurring = data.Reoccurring;
        Name = data.Name;
        ColorCode = data.ColorCode;
    }

    public void Start() {
        LoadData(defaultData);
    }
}
