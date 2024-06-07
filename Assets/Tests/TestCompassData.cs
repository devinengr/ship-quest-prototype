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

    // [Test]
    // public void testOptimizeValueForAveraging() {
    //     Assert.AreEqual(380, CompassLogic.OptimizeValueForAveraging(350, 20));
    //     Assert.AreEqual(10, CompassLogic.OptimizeValueForAveraging(20, 10));
    //     Assert.AreEqual(0, CompassLogic.OptimizeValueForAveraging(0, 0));
    //     Assert.AreEqual(359, CompassLogic.OptimizeValueForAveraging(359, 359));
    //     Assert.AreEqual(358, CompassLogic.OptimizeValueForAveraging(359, 358));
    //     Assert.AreEqual(360, CompassLogic.OptimizeValueForAveraging(359, 0));
    //     Assert.AreEqual(361, CompassLogic.OptimizeValueForAveraging(300, 1));
    // }

    // [Test]
    // public void testAdjustedAverage() {
    //     Assert.AreEqual(15, CompassLogic.AdjustedAverage(10, 20));
    //     Assert.AreEqual(0, CompassLogic.AdjustedAverage(0, 0));
    //     Assert.AreEqual(360, CompassLogic.AdjustedAverage(360, 360));
    //     Assert.AreEqual(360, CompassLogic.AdjustedAverage(0, 360));
    //     Assert.AreEqual(360, CompassLogic.AdjustedAverage(360, 0));
    //     Assert.AreEqual(350, CompassLogic.AdjustedAverage(320, 20));
    //     Assert.AreEqual(370, CompassLogic.AdjustedAverage(340, 40));
    //     Assert.AreEqual(360, CompassLogic.AdjustedAverage(359, 1));
    //     Assert.AreEqual(361, CompassLogic.AdjustedAverage(359, 3));
    //     Assert.AreEqual(1, CompassLogic.AdjustedAverage(0, 2));
    // }
    
}
