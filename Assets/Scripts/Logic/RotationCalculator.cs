using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class RotationCalculator {

    private static List<float> rotationList = new();

    #region Instance Methods

    private static void EnsureFlexibility(int limit) {
        while (rotationList.Count > limit) {
            RemoveRotation();
        }
    }

    private static void RemoveRotation() {
        rotationList.RemoveAt(0);
    }

    public static List<float> SortRotations() {
        List<float> sorted = new();
        foreach (float rotation in rotationList) {
            sorted.Add(rotation);
        }
        sorted.Sort();
        return sorted;
    }

    #endregion

    #region Public Methods

    public static void AddRotation(float degrees, int limit) {
        rotationList.Add(degrees);
        EnsureFlexibility(limit);
    }

    public static void Clear() {
        rotationList.Clear();
    }

    public static float CalcAvgRotation() {
        float avgDeg = 0f;
        float last = 0f;
        foreach (float rotation in SortRotations()) {
            last = CompassLogic.AdjustNewReading(last, rotation);
            avgDeg += last;
        }
        avgDeg /= rotationList.Count;
        return avgDeg;
    }

    #endregion

}
