using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
public class ExpensesManager : MonoBehaviour, ManagerInterface {
    [SerializeField] private Expense OriginalExpense = null;


    public ManagerStatus status { get; private set; }

    public void Startup() {
        Debug.Log("Expenses manager starting...");

        status = ManagerStatus.Started;
    }

    public void LoadData(List<ExpenseData> expenseDatas) {
        var sortedExpenseDatas = expenseDatas.GroupBy(expense => expense.ID);
        foreach (var expenseDataGroup in sortedExpenseDatas)
            if (Managers.Catagory.Catagories.ContainsKey(expenseDataGroup.Key))
                Managers.Catagory.Catagories[expenseDataGroup.Key].LoadExpenses(expenseDataGroup.ToList<ExpenseData>());
            else
                Debug.Log($"ERROR: Catagory ID {expenseDataGroup.Key} could not be found. Expense Data was lost.");
        Messenger.Broadcast(CatagoryEvent.EXPENSES_UPDATED);
    }

    public List<ExpenseData> GetData() {
        List<ExpenseData> expenseDatas = new List<ExpenseData>();
        foreach (KeyValuePair<int, Catagory> catagory in Managers.Catagory.Catagories)
            expenseDatas.AddRange(catagory.Value.GetExpenseDatas());
        return expenseDatas;
    }
    
}*/
