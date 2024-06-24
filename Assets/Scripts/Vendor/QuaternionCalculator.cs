using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

public sealed class RotationCalculator {

    private static List<Vector3> rotationList = new();

    public static void AddRotation(Vector3 rotation) {
        rotationList.Add(rotation);
    }

    public static void ClearRotations() {
        rotationList.Clear();
    }

    public static Quaternion CalcAvgRotation() {
        float avgDeg = 0f;
        float lastY = 0f;
        foreach (Vector3 rotation in rotationList) {
            // i'm not sure if this is completely correct but it should
            // prevent wrapping issues (averaging 20 and 360, for instance)
            lastY = CompassLogic.AdjustNewReading(lastY, rotation.y);
            avgDeg += lastY;
        }
        avgDeg /= rotationList.Count;
        return Quaternion.Euler(0f, avgDeg, 0f);
    }

}

public sealed class QuaternionCalculator {

    private static List<Quaternion> quaternionList = new();

    public static void AddQuaternion(Quaternion quaternion) {
        quaternionList.Add(quaternion);
    }

    public static void ClearQuaternions() {
        quaternionList.Clear();
    }

    /// <summary>
    /// Code is from https://gamedev.stackexchange.com/a/119871
    /// Author: sam hocevar
    /// </summary>
    public static Quaternion CalcAvgQuaternion() {
        if (quaternionList.Count == 0) {
            throw new ArgumentException();
        }
        float x = 0, y = 0, z = 0, w = 0;
        foreach (Quaternion q in quaternionList) {
            x += q.x;
            y += q.y;
            z += q.z;
            w += q.w;
        }
        float k = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
        return new Quaternion(x * k, y * k, z * k, w * k);
    }

}
