using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class FileData {

    [Key(0)]
    List<ExpenseData> Expenses { get; set; } = new List<ExpenseData>();

    [Key(1)]
    decimal Goal { get; set; } = 1000.00m;
}