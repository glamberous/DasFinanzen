using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New CatagoryData", menuName = "CatagoryData")]
public class CatagoryData : ScriptableObject {
    public bool Reoccurring;
    public string Name;
    public string ColorCode;
}   