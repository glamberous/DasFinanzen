using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.UI;


[System.Serializable]
[CreateAssetMenu(fileName = "New AppData", menuName = "AppData")]
public class AppData : ScriptableObject {
    public CatagoryData[] MonthlyCatagoryData;
    public CatagoryData[] DailyCatagoryData;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New CatagoryData", menuName = "CatagoryData")]
public class CatagoryData : ScriptableObject{
    public bool Reoccurring;
    public string NameText;
    public string ColorCode;
    public ExpenseData[] ExpenseData;
}

[System.Serializable]
public class ExpenseData {
    public int Date;
    public decimal Amount;
    public string Name;
}

//public static string[] Colors = new string[] { "D7DAE0", "ADCE1B", "60B9BD", "4483AA", "10B1FF", "FF6481", "9F7DFF", "D3D2A2", "CE9986", "94B57C", "ffb86c", "FF79C6" };
//public static string[] Catagories = new string[] { "Savings", "Groceries", "Restaurant", "Coffee", "Fun", "Misc", "Emergency", "Roth IRA", "Mortgage", "Extra Mortgage", "HOA", "Game" };
