using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ColorConverterTests
    {
        [Test]
        public void HexToColor_FFFFFF() {
            Color MyColor = ColorConverter.HexToColor("FFFFFF");
            Assert.AreEqual(1.0f, MyColor.r);
            Assert.AreEqual(1.0f, MyColor.g);
            Assert.AreEqual(1.0f, MyColor.b);
        }

        [Test]
        public void HexToColor_000000() {
            Color MyColor = ColorConverter.HexToColor("000000");
            Assert.AreEqual(0.0f, MyColor.r);
            Assert.AreEqual(0.0f, MyColor.g);
            Assert.AreEqual(0.0f, MyColor.b);
        }

        [Test]
        public void HexToColor_123456() {
            Color MyColor = ColorConverter.HexToColor("123456");
            Assert.AreEqual(0.0705882385f, MyColor.r);
            Assert.AreEqual(0.203921571f, MyColor.g);
            Assert.AreEqual(0.337254912f, MyColor.b);
        }

        [Test]
        public void ColorToHex_White() {
            Color myColor = new Color(1.0f, 1.0f, 1.0f);
            string Result = ColorConverter.ColorToHex(myColor);
            Assert.AreEqual("FFFFFF", Result);
        }

        [Test]
        public void ColorToHex_Black() {
            Color myColor = new Color(0.0f, 0.0f, 0.0f);
            string Result = ColorConverter.ColorToHex(myColor);
            Assert.AreEqual("000000", Result);
        }
    }
}
