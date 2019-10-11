using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class FileData {

    [Key(0)]
    public List<ExpenseData> ExpenseDatas = new List<ExpenseData>();

    [Key(1)]
    public decimal Goal = 1000.00m;
}