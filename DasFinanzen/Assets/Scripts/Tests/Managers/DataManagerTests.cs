using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.IO;

//###############################################################################################################
//#### IMPORTANT NOTE: 
//#### These tests depend on the files within the Assets/Scripts/Tests/TestData folder.
//###############################################################################################################

/*
namespace Tests {
    public class DataManagerTests {
        DataManager Data = null;
        List<CatagoryData> testCatagoryDataList = new List<CatagoryData>();
        CatagoryData testCatagoryData0 = ScriptableObject.CreateInstance<CatagoryData>();
        CatagoryData testCatagoryData1 = ScriptableObject.CreateInstance<CatagoryData>();

        bool WasBroadcastCalled = false;
        public void CallBroadcast() => WasBroadcastCalled = true;

        [SetUp]
        public void SetUp() {
            testCatagoryData0.LoadTestData(reoccurring: false, id: 0, nameText: "Testing_0", colorCode: "AABBCC");
            testCatagoryData1.LoadTestData(reoccurring: true, id: 1, nameText: "Testing_1", colorCode: "001122");
            testCatagoryDataList.Add(testCatagoryData0);
            testCatagoryDataList.Add(testCatagoryData1);
            Data = new DataManager(testCatagoryDataList);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/valid_data.fin");
            Messenger.AddListener(AppEvent.EXPENSES_UPDATED, CallBroadcast);
        }

        [TearDown]
        public void TearDown() {
            Data = null;
            List<CatagoryData> testCatagoryDataList = new List<CatagoryData>();
            CatagoryData testCatagoryData1 = ScriptableObject.CreateInstance<CatagoryData>();
            CatagoryData testCatagoryData2 = ScriptableObject.CreateInstance<CatagoryData>();

            File.Delete(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Messenger.RemoveListener(AppEvent.EXPENSES_UPDATED, CallBroadcast);
            WasBroadcastCalled = false;
        }
        
        [Test]
        public void DataTest_Test_File_Exists() => FileAssert.Exists(Application.dataPath + "/Scripts/Tests/TestData/valid_data.fin");
        
        [Test]
        public void LoadGameState_Loads_Empty_Profile_When_Invalid_File() {
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/invalid_data.fin");
            Data.LoadGameState();
            bool isEmpty = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in Data.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 1)
                    isEmpty = false;
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void LoadGameState_Loads_Goal_From_File() {
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/999BudgetGoal_data.fin");
            Data.LoadGameState();
            Assert.AreEqual(999.99m, Data.BudgetGoal);
        }
        
        [Test]
        public void LoadGameState_Loads_An_Expense() {
            Data.LoadGameState();
            bool hasExpense = false;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in Data.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 0)
                    hasExpense = true;
            Assert.IsTrue(hasExpense);
        }

        [Test]
        public void LoadGameState_ExpenseDatasDict_Has_Same_Num_Of_Keys_As_CatagoryDataDict() {
            Data.LoadGameState();
            Assert.AreEqual(Data.CatagoryDataDict.Count, Data.ExpenseDatasDict.Count);
        }

        [Test]
        public void LoadGameState_ExpenseDatasDict_Has_Same_Keys_As_CatagoryDataDict() {
            Data.LoadGameState();
            bool hasSameKeys = true;
            foreach (KeyValuePair<int, CatagoryData> catagoryData in Data.CatagoryDataDict)
                if (!Data.ExpenseDatasDict.ContainsKey(catagoryData.Key))
                    hasSameKeys = false;
            Assert.IsTrue(hasSameKeys);
        }

        [Test]
        public void LoadGameState_CatagoryDataDict_Has_Same_Keys_As_ExpenseDatasDict() {
            Data.LoadGameState();
            bool hasSameKeys = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseData in Data.ExpenseDatasDict)
                if (!Data.CatagoryDataDict.ContainsKey(expenseData.Key))
                    hasSameKeys = false;
            Assert.IsTrue(hasSameKeys);
        }

        [Test]
        public void LoadGameState_Loads_Expenses_Into_Expected_Dictionary_Key() {
            Data.LoadGameState();
            bool allExpensesCatagorizedProperly = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in Data.ExpenseDatasDict)
                foreach (ExpenseData expenseData in expenseDatas.Value)
                    if (expenseData.ID != expenseDatas.Key)
                        allExpensesCatagorizedProperly = false;
            Assert.IsTrue(allExpensesCatagorizedProperly);
        }

        [Test]
        public void LoadGameState_Loads_Empty_Profile_When_Missing_File() {
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/missing_data.fin");
            Data.LoadGameState();
            bool isEmpty = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in Data.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 1)
                    isEmpty = false;
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void SaveGameState_Creates_File() {
            Data.LoadGameState();
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.SaveGameState();
            FileAssert.Exists(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
        }

        [Test]
        public void SaveGameState_Data_Retained_After_Save_Then_Reload() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(1337, "Testing Save/Load", 9.99m, 0);
            Data.ExpenseDatasDict[newExpenseData.ID] = new List<ExpenseData>();
            Data.ExpenseDatasDict[newExpenseData.ID].Add(newExpenseData);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.SaveGameState();
            Data.LoadGameState();
            Assert.AreEqual(1337, Data.ExpenseDatasDict[0][0].EpochDate);
            Assert.AreEqual("Testing Save/Load", Data.ExpenseDatasDict[0][0].NameText);
            Assert.AreEqual(9.99m, Data.ExpenseDatasDict[0][0].Amount);
            Assert.AreEqual(0, Data.ExpenseDatasDict[0][0].ID);
        }

        [Test]
        public void GetExpensesTotal_Returns_Total_Expenses_For_Catagory_ID() {
            ExpenseData newExpenseData1 = new ExpenseData();
            ExpenseData newExpenseData2 = new ExpenseData();
            newExpenseData1.LoadTestData(0, "Testing GetExpenseTotal", 10.50m, 0);
            newExpenseData2.LoadTestData(0, "Testing GetExpenseTotal", 10.50m, 0);
            Data.ExpenseDatasDict[0] = new List<ExpenseData>();
            Data.ExpenseDatasDict[0].Add(newExpenseData1);
            Data.ExpenseDatasDict[0].Add(newExpenseData2);
            decimal total = Data.GetExpensesTotal(0);
            Assert.AreEqual(21.00m, total);
        }

        [Test]
        public void GetExpensesTotal_Returns_0_If_ID_Is_Invalid() {
            decimal total = Data.GetExpensesTotal(150);
            Assert.AreEqual(0.00m, total);
        }

        [Test]
        public void CurrentExpenseDatas_Returns_Null_If_ID_Is_Invalid() {
            Data.CurrentID = 20;
            Assert.AreEqual(null, Data.CurrentExpenseDatas);
        }

        [Test]
        public void CurrentExpenseDatas_Returns_Current_Expense_Datas() {
            Data.CurrentID = 1;
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData);
            Assert.AreEqual(Data.ExpenseDatasDict[1], Data.CurrentExpenseDatas);
        }

        [Test]
        public void CurrentCatagoryData_Returns_Null_If_ID_Is_Invalid() {
            Data.CurrentID = 20;
            Assert.AreEqual(null, Data.CurrentCatagoryData);
        }

        [Test]
        public void CurrentCatagoryData_Returns_Current_Catagory_Data() {
            Data.CurrentID = 1;
            Assert.AreEqual(Data.CatagoryDataDict[1], Data.CurrentCatagoryData);
        }

        [Test]
        public void AddExpense_Expense_Is_Added() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            int num_of_expenses = Data.ExpenseDatasDict[1].Count;
            Data.AddExpense(newExpenseData);
            Assert.AreEqual(num_of_expenses + 1, Data.ExpenseDatasDict[1].Count);
        }

        [Test]
        public void AddExpense_Save_Is_Triggered() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.AddExpense(newExpenseData);
            FileAssert.Exists(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
        }

        
        [Test]
        public void AddExpense_Expenses_Updated_Broadcast_Is_Sent() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.AddExpense(newExpenseData);
            Assert.IsTrue(WasBroadcastCalled);
        }

        [Test]
        public void EditExpense_Expense_Is_Edited() {
            ExpenseData newExpenseData1 = new ExpenseData();
            ExpenseData newExpenseData2 = new ExpenseData();
            newExpenseData1.LoadTestData(0, "Testing1", 11.11m, 1);
            newExpenseData2.LoadTestData(0, "Testing2", 22.22m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData1);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.EditExpense(newExpenseData1, newExpenseData2);
            Assert.AreEqual(newExpenseData2.EpochDate, Data.ExpenseDatasDict[1][0].EpochDate);
            Assert.AreEqual(newExpenseData2.NameText, Data.ExpenseDatasDict[1][0].NameText);
            Assert.AreEqual(newExpenseData2.Amount, Data.ExpenseDatasDict[1][0].Amount);
            Assert.AreEqual(newExpenseData2.ID, Data.ExpenseDatasDict[1][0].ID);
        }

        [Test]
        public void EditExpense_Save_Is_Triggered() {
            ExpenseData newExpenseData1 = new ExpenseData();
            ExpenseData newExpenseData2 = new ExpenseData();
            newExpenseData1.LoadTestData(0, "Testing1", 11.11m, 1);
            newExpenseData2.LoadTestData(0, "Testing2", 22.22m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData1);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.EditExpense(newExpenseData1, newExpenseData2);
            FileAssert.Exists(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
        }

        [Test]
        public void EditExpense_Expenses_Updated_Broadcast_Is_Sent() {
            ExpenseData newExpenseData1 = new ExpenseData();
            ExpenseData newExpenseData2 = new ExpenseData();
            newExpenseData1.LoadTestData(0, "Testing1", 11.11m, 1);
            newExpenseData2.LoadTestData(0, "Testing2", 22.22m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData1);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.EditExpense(newExpenseData1, newExpenseData2);
            Assert.IsTrue(WasBroadcastCalled);
        }

        [Test]
        public void RemoveExpense_Expense_Is_Removed() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData);
            int num_of_expenses = Data.ExpenseDatasDict[1].Count;
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.RemoveExpense(newExpenseData);
            Assert.AreEqual(num_of_expenses - 1, Data.ExpenseDatasDict[1].Count);
        }

        [Test]
        public void RemoveExpense_Save_Is_Triggered() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.RemoveExpense(newExpenseData);
            FileAssert.Exists(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
        }

        [Test]
        public void RemoveExpense_Expenses_Updated_Broadcast_Is_Sent() {
            ExpenseData newExpenseData = new ExpenseData();
            newExpenseData.LoadTestData(0, "Testing", 10.50m, 1);
            Data.ExpenseDatasDict[1] = new List<ExpenseData>();
            Data.ExpenseDatasDict[1].Add(newExpenseData);
            Data.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            Data.RemoveExpense(newExpenseData);
            Assert.IsTrue(WasBroadcastCalled);
        }
    }
}
*/