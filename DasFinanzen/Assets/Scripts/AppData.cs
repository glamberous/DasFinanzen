using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expense : ScriptableObject {
    public int Date;
    public decimal Amount;
    public string Name;
}

public class Catagory : ScriptableObject {
    private readonly string Name;
    public Catagory(string name) => Name = name;
    private string Color;

    public List<Expense> Expenses;
}

public class MonthlyCatagory : Catagory {
    public MonthlyCatagory(string name) : base(name) { }
    private const bool Reoccurring = true;
}

public class DailyCatagory : Catagory {
    public DailyCatagory(string name) : base(name) { }
    private const bool Reoccurring = false;
}

//public static string[] Colors = new string[] { "D7DAE0", "ADCE1B", "60B9BD", "4483AA", "10B1FF", "FF6481", "9F7DFF", "D3D2A2", "CE9986", "94B57C", "ffb86c", "FF79C6" };
//public static string[] Catagories = new string[] { "Savings", "Groceries", "Restaurant", "Coffee", "Fun", "Misc", "Emergency", "Roth IRA", "Mortgage", "Extra Mortgage", "HOA", "Game" };
