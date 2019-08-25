using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ExpenseData", menuName = "ExpenseData")]
[System.Serializable]
public class ExpenseData : ScriptableObject {
    public int EpochDate;
    public string NameText;
    public decimal Amount = 1.00m;
    public int CatagoryID;
}
