using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

namespace Tests {
    public class ErrorManagerTests {
        ErrorManager Error = null;
        GameObject Object1 = null;
        GameObject Object2 = null;
        Image imageObject = null;
        TextMeshProUGUI textObject = null;

        [SetUp]
        public void SetUp() {
            Object1 = new GameObject();
            Object2 = new GameObject();
            Object1.AddComponent<Image>();
            Object2.AddComponent<TextMeshProUGUI>();
            imageObject = Object1.GetComponent<Image>();
            textObject = Object2.GetComponent<TextMeshProUGUI>();
            Error = new ErrorManager(imageObject, textObject);
        }

        [TearDown]
        public void TearDown() {
            Object1 = null;
            Object2 = null;
            imageObject = null;
            textObject = null;
            Error = null;
        }

        [Test]
        public void TryGetImage_Returns_Error_Variable_On_Null() {
            Image test = Error.TryGetImage(null);
            Assert.IsTrue(GameObject.ReferenceEquals(imageObject, test));
        }

        [Test]
        public void TryGetTextMeshProUGUI_Returns_Error_Variable_On_Null() {
            TextMeshProUGUI test = Error.TryGetTextMeshProUGUI(null);
            Assert.IsTrue(GameObject.ReferenceEquals(textObject, test));
        }

        [Test]
        public void TryGetImage_Same_Object_Entered_Is_Returned_When_No_Error() {
            GameObject newObject = new GameObject();
            newObject.AddComponent<Image>();
            Image testObject = newObject.GetComponent<Image>();
            Image test = Error.TryGetImage(testObject);
            Assert.IsTrue(GameObject.ReferenceEquals(testObject, test));
        }

        [Test]
        public void TryGetTextMeshProUGUI_Same_Object_Entered_Is_Returned_When_No_Error() {
            GameObject newObject = new GameObject();
            newObject.AddComponent<TextMeshProUGUI>();
            TextMeshProUGUI testObject = newObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI test = Error.TryGetTextMeshProUGUI(testObject);
            Assert.IsTrue(GameObject.ReferenceEquals(testObject, test));
        }
    }
}
