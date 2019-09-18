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
        GameObject Object2 = null;
        TextMeshProUGUI textMesh = null;
        TMP_InputField inputField = null;

        [SetUp]
        public void SetUp() {
            Object1 = new GameObject();
            Object1.AddComponent<TextMeshProUGUI>();
            textMesh = Object1.GetComponent<TextMeshProUGUI>();
            Object2 = new GameObject();
            Object2.AddComponent<TMP_InputField>();
            inputField = Object2.GetComponent<TMP_InputField>();
            EditExpense = new EditExpenseManager(null, null, inputField, textMesh, null);
        }

        [TearDown]
        public void TearDown() {
            EditExpense = null;
            Object1 = null;
            Object2 = null;
            textMesh = null;
            inputField = null;
        }

        [Test]
        public void ConvertAmountForDisplay_Absurd_Right_Digits() {
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
        public void UpdateEditExpenseAmount_Converts_String_With_Commas() {
            EditExpense.InitializeTempExpense();
            EditExpense.SetAmountTextProxyText("4,000.00");
            EditExpense.UpdateEditExpenseAmount();
            decimal result = EditExpense.GetTempExpenseAmount();
            Assert.AreEqual(4000.00m, result);
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

        [Test]
        public void AmountOnValueChanged_Entering_Zero_Immediately_Removes_The_Zero() {
            EditExpense.SetAmountInputFieldText("0");
            EditExpense.AmountOnValueChanged();
            string result = EditExpense.GetAmountInputFieldText();
            Assert.AreEqual("", result);
        }

        [Test]
        public void AmountOnValueChanged_Dollar_AKA_Checking_Decimal_Point() {
            EditExpense.SetAmountInputFieldText("100");
            EditExpense.AmountOnValueChanged();
            string result = EditExpense.GetAmountTextProxyText();
            Assert.AreEqual("1.00", result);
        }

        [Test]
        public void AmountOnValueChanged_One_Thousand_AKA_Checking_Comma() {
            EditExpense.SetAmountInputFieldText("100000");
            EditExpense.AmountOnValueChanged();
            string result = EditExpense.GetAmountTextProxyText();
            Assert.AreEqual("1,000.00", result);
        }

        [Test]
        public void AmountOnValueChanged_Nothing() {
            EditExpense.SetAmountInputFieldText("");
            EditExpense.AmountOnValueChanged();
            string result = EditExpense.GetAmountTextProxyText();
            Assert.AreEqual("0.00", result);
        }
    }
}
