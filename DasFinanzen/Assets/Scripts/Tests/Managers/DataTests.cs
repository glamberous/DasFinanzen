using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//###############################################################################################################
//#### IMPORTANT NOTE: These tests depend on the data.fin that exists in 
//###############################################################################################################

namespace Tests
{
    public class Data
    {
        DataManager MyDataManager = new DataManager();
        List<CatagoryData> testCatagoryDataList = new List<CatagoryData>();
        CatagoryData testCatagoryData1 = ScriptableObject.CreateInstance<CatagoryData>();
        CatagoryData testCatagoryData2 = ScriptableObject.CreateInstance<CatagoryData>();

        [SetUp]
        public void SetUp() {
            testCatagoryData1.LoadTestData(reoccurring: false, id: 1, nameText: "Testing_1", colorCode: "AABBCC");
            testCatagoryData2.LoadTestData(reoccurring: true, id: 2, nameText: "Testing_2", colorCode: "001122");
            testCatagoryDataList.Add(testCatagoryData1);
            testCatagoryDataList.Add(testCatagoryData2);
            MyDataManager.SetTestCatagoryData(testCatagoryDataList);
            MyDataManager.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/valid_data.fin");
        }

        [TearDown]
        public void TearDown() {
            MyDataManager = new DataManager();
            List<CatagoryData> testCatagoryDataList = new List<CatagoryData>();
            CatagoryData testCatagoryData1 = ScriptableObject.CreateInstance<CatagoryData>();
            CatagoryData testCatagoryData2 = ScriptableObject.CreateInstance<CatagoryData>();
        }

        [Test]
        public void LoadGameState_Can_Find_File() => FileAssert.Exists(MyDataManager.GetFilePath());

        [Test]
        public void LoadGameState_Loads_Empty_Profile_When_Invalid_File() {
            MyDataManager.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/invalid_data.fin");
            MyDataManager.LoadGameState();
            bool isEmpty = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in MyDataManager.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 1)
                    isEmpty = false;
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void LoadGameState_Loads_Goal_From_File() {
            MyDataManager.LoadGameState();
            Assert.AreEqual(900.00m, MyDataManager.BudgetGoal);
        }
        
        [Test]
        public void SaveGameState_Creates_File() {
            MyDataManager.LoadGameState();
            MyDataManager.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/save_test_data.fin");
            //ExpenseData testExpenseData = new ExpenseData();
            //testExpenseData.LoadTestData(epochDate: 0, nameText: "SaveExpenseTest", amount: 0.50m, id: 1);
            //MyDataManager.AddExpense(testExpenseData);
            MyDataManager.SaveGameState();
            FileAssert.Exists(MyDataManager.GetFilePath());
        } 
        
        [Test]
        public void LoadGameState_Loads_An_Expense() {
            MyDataManager.LoadGameState();
            bool isEmpty = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in MyDataManager.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 1)
                    isEmpty = false;
            Assert.IsFalse(isEmpty);
        }

        [Test]
        public void LoadGameState_ExpenseDataDict_Has_Same_Num_Of_Keys_As_CatagoryDataDict() {
            MyDataManager.LoadGameState();
            Assert.AreEqual(MyDataManager.CatagoryDataDict.Count, MyDataManager.ExpenseDatasDict.Count);
        }

        [Test]
        public void LoadGameState_ExpenseDatasDict_Has_Same_Keys_As_CatagoryDataDict() {
            MyDataManager.LoadGameState();
            bool hasSameKeys = true;
            foreach (KeyValuePair<int, CatagoryData> catagoryData in MyDataManager.CatagoryDataDict)
                if (!MyDataManager.ExpenseDatasDict.ContainsKey(catagoryData.Key))
                    hasSameKeys = false;
            Assert.IsTrue(hasSameKeys);
        }

        [Test]
        public void LoadGameState_CatagoryDataDict_Has_Same_Keys_As_ExpenseDatasDict() {
            MyDataManager.LoadGameState();
            bool hasSameKeys = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseData in MyDataManager.ExpenseDatasDict)
                if (!MyDataManager.CatagoryDataDict.ContainsKey(expenseData.Key))
                    hasSameKeys = false;
            Assert.IsTrue(hasSameKeys);
        }

        [Test]
        public void LoadGameState_Loads_Expenses_Into_Expected_Dictionary_Key() {
            MyDataManager.LoadGameState();
            bool allExpensesCatagorizedProperly = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in MyDataManager.ExpenseDatasDict)
                foreach (ExpenseData expenseData in expenseDatas.Value)
                    if (expenseData.ID != expenseDatas.Key)
                        allExpensesCatagorizedProperly = false;
            Assert.IsTrue(allExpensesCatagorizedProperly);
        }

        [Test]
        public void LoadGameState_Loads_Empty_Profile_When_Missing_File() {
            MyDataManager.SetFilePath(Application.dataPath + "/Scripts/Tests/TestData/missing_data.fin");
            MyDataManager.LoadGameState();
            bool isEmpty = true;
            foreach (KeyValuePair<int, List<ExpenseData>> expenseDatas in MyDataManager.ExpenseDatasDict)
                if (expenseDatas.Value.Count > 1)
                    isEmpty = false;
            Assert.IsTrue(isEmpty);
        }
        /*

        [Test]
        public void SaveGameState_ProperlySavesVariables() {

        }

        [Test]
        public void GetExpensesTotal_ReturnsExpensesTotal() {
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator DataWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        */
    }
}
