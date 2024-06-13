using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GyroData : MonoBehaviour {

    public Quaternion currentGyro { get; set; }

    public float yaw { get; set; }

    void Start() {
        Input.gyro.enabled = true;
    }

    void Update() {
        currentGyro = GyroToUnity(Input.gyro.attitude);
        float x = currentGyro.x;
        float y = currentGyro.y;
        float z = currentGyro.z;
        float w = currentGyro.w;

        // https://discussions.unity.com/t/finding-pitch-roll-yaw-from-quaternions/65684/3
        yaw = Mathf.Asin(2*x*y + 2*z*w);
    }

    // https://docs.unity3d.com/ScriptReference/Gyroscope.html
    Quaternion GyroToUnity(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

}
