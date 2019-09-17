using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

namespace Tests
{
    public class EditExpenseManagerTests
    {
        EditExpenseManager EditExpense = null;
        GameObject Object1 = null;
        TextMeshProUGUI textMesh = null;

        [SetUp]
        public void SetUp() {
            Object1 = new GameObject();
            Object1.AddComponent<TextMeshProUGUI>();
            textMesh = Object1.GetComponent<TextMeshProUGUI>();
            EditExpense = new EditExpenseManager(null, null, null, textMesh, null);
        }

        [TearDown]
        public void TearDown() {
            EditExpense = null;
            Object1 = null;
            textMesh = null;
        }

        [Test]
        public void ConvertAmountForDisplay_Absurd_Right_Digits()
        {
            string result = EditExpense.ConvertAmountForDisplay(0.00000000000001m);
            Assert.AreEqual("0", result);
        }

        [Test]
        public void ConvertAmountForDisplay_Absurd_Left_Digits() {
            string result = EditExpense.ConvertAmountForDisplay(10000000.00m);
            Assert.AreEqual("1000000000", result);
        }

        [Test]
        public void ConvertAmountForDisplay_No_Decimal() {
            string result = EditExpense.ConvertAmountForDisplay(999.99m);
            Assert.AreEqual("99999", result);
        }

        [Test]
        public void UpdateEditExpenseAmount_Decimal_String_Entry() {
            EditExpense.InitializeTempExpense();
            EditExpense.SetAmountTextProxyText("40.00");
            EditExpense.UpdateEditExpenseAmount();
            decimal result = EditExpense.GetTempExpenseAmount();
            Assert.AreEqual(40.00m, result);
        }

        [Test]
        public void UpdateEditExpenseAmount_Alpha_String_Entry() {
            EditExpense.InitializeTempExpense();
            EditExpense.SetAmountTextProxyText("FourtyTwo");
            EditExpense.UpdateEditExpenseAmount();
            decimal result = EditExpense.GetTempExpenseAmount();
            Assert.AreEqual(0.00m, result);
        }

        [Test]
        public void UpdateEditExpenseAmount_Integer_String_Entry() {
            EditExpense.InitializeTempExpense();
            EditExpense.SetAmountTextProxyText("100");
            EditExpense.UpdateEditExpenseAmount();
            decimal result = EditExpense.GetTempExpenseAmount();
            Assert.AreEqual(100.00m, result);
        }
    }
}
