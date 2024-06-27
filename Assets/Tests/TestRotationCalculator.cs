using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestTools;

public class TestDegreeCalculator {

    private static readonly float DELTA = 0.00001f;

    private float d1 = -20;
    private float d2 = 20;
    private float d3 = 60;
    private float d4 = 340;
    private float d5 = 380;
    private float d6 = 400;

    [PreTest]
    public void SetUp() {
        RotationCalculator.Clear();
    }

    [Test]
    public void TestAverageSimple() {
        RotationCalculator.AddRotation(d2, 3);
        RotationCalculator.AddRotation(d3, 3);
        RotationCalculator.AddRotation(d3, 3);
        Assert.AreEqual(46.66666, RotationCalculator.CalcAvgRotation(), DELTA);
    }

    [Test]
    public void TestLimitUsesMostRecentValues() {
        RotationCalculator.AddRotation(d2, 3);
        RotationCalculator.AddRotation(d3, 3);
        RotationCalculator.AddRotation(d4, 3);
        RotationCalculator.AddRotation(d5, 3);
        Assert.AreEqual(20, RotationCalculator.CalcAvgRotation(), DELTA);
    }

    [Test]
    public void TestWrapCloseLow() {
        RotationCalculator.AddRotation(d1, 3);
        RotationCalculator.AddRotation(d2, 3);
        RotationCalculator.AddRotation(d3, 3);
        Assert.AreEqual(20, RotationCalculator.CalcAvgRotation(), DELTA);
    }

    [Test]
    public void TestWrapCloseHigh() {
        RotationCalculator.AddRotation(d4, 3);
        RotationCalculator.AddRotation(d5, 3);
        RotationCalculator.AddRotation(d6, 3);
        Assert.AreEqual(13.33333, RotationCalculator.CalcAvgRotation(), DELTA);
    }

    [Test]
    public void TestWrapFar() {
        RotationCalculator.AddRotation(d1, 4);
        RotationCalculator.AddRotation(d2, 4);
        RotationCalculator.AddRotation(d5, 4);
        RotationCalculator.AddRotation(d6, 4);
        Assert.AreEqual(15, RotationCalculator.CalcAvgRotation(), DELTA);
    }

}
