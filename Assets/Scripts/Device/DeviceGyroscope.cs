using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceGyroscope : MonoBehaviour {
    
    public bool DeviceGyroscopeIsEnabled { get {
        return SystemInfo.supportsGyroscope || Application.isEditor; } }

    // Code by David: https://stackoverflow.com/a/68326128
    private Quaternion GyroToUnity(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    // Code by David: https://stackoverflow.com/a/68326128
    public Quaternion Attitude() {
        Quaternion rawAttitude = Input.gyro.attitude;
        Quaternion attitude = GyroToUnity(rawAttitude);
        return attitude;
    }

    void Start() {
        Input.gyro.enabled = true;
    }

}
