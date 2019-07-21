using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UI_Tests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void UI_TestsSimplePasses()
        {
            bool Koda = true;
            // Use the Assert class to test conditions
            Assert.IsTrue(Koda);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator UI_TestsWithEnumeratorPasses()
        {
            bool Gizmo = false;
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;

            Assert.IsTrue(Gizmo);
        }
    }
}
