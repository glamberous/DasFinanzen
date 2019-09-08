using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CatagoryData", menuName = "CatagoryData")]
public class CatagoryData : ScriptableObject {
    public bool Reoccurring;
    public string NameText;
    public string ColorCode;
    public int ID;

#if UNITY_EDITOR
    public void LoadTestData(bool reoccurring, string nameText, string colorCode, int id) {
        Reoccurring = reoccurring;
        NameText = nameText;
        ColorCode = colorCode;
        ID = id;
    }
#endif
}