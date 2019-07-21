using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public struct AppData {
    public Dictionary<int, Dictionary<int, List<Expense>>> 
}*/

public class Expense {
    public int Day { get; set; }
    public decimal Amount { get; set; }
    public string Name { get; set; }
}

public class MonthData {
    public Dictionary<int, List<Expense>> months;
}

public class YearData {
    public Dictionary<int, MonthData> years;
}

public class Test {
    Expense new_expense;
    public Test() => new_expense = new Expense();
}
