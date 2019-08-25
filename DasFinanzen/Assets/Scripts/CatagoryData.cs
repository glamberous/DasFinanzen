using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CatagoryData", menuName = "CatagoryData")]
public class CatagoryData : ScriptableObject {
    public bool Reoccurring;
    public string NameText;
    public string ColorCode;
    public int CatagoryID;
}