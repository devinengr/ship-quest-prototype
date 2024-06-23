using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCompassData : MonoBehaviour {

    [Test]
    public void testAdjustNewReading() {
        // Normal
        Assert.AreEqual(20, CompassLogic.AdjustNewReading(10, 20));
        Assert.AreEqual(100, CompassLogic.AdjustNewReading(10, 100));
        // Wrapping
        Assert.AreEqual(420, CompassLogic.AdjustNewReading(340, 60));
        Assert.AreEqual(361, CompassLogic.AdjustNewReading(359, 1));
        Assert.AreEqual(-1, CompassLogic.AdjustNewReading(1, 359));
        Assert.AreEqual(-1, CompassLogic.AdjustNewReading(0, 359));
        Assert.AreEqual(360, CompassLogic.AdjustNewReading(359, 0));
        // Distance > 180
        Assert.AreEqual(190, CompassLogic.AdjustNewReading(10, 190));
        Assert.AreEqual(-169, CompassLogic.AdjustNewReading(10, 191));
        Assert.AreEqual(359, CompassLogic.AdjustNewReading(179, 359));
        Assert.AreEqual(-1, CompassLogic.AdjustNewReading(178, 359));
        // Wrapped multiple times
        Assert.AreEqual(380, CompassLogic.AdjustNewReading(500, 20));
        Assert.AreEqual(780, CompassLogic.AdjustNewReading(770, 60));
        // Wrapped multiple times and distance > 180
        Assert.AreEqual(840, CompassLogic.AdjustNewReading(840, 120));
        Assert.AreEqual(839, CompassLogic.AdjustNewReading(840, 119));
        // Opposite direction
        Assert.AreEqual(-210, CompassLogic.AdjustNewReading(-200, 150));
        Assert.AreEqual(-370, CompassLogic.AdjustNewReading(-350, 350));
        Assert.AreEqual(-590, CompassLogic.AdjustNewReading(-650, 130));
    }
    
}
