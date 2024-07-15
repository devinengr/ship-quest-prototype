using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceGyroscope : MonoBehaviour {

    public Camera mainCamera;
    
    public bool DeviceGyroscopeIsEnabled { get {
        return SystemInfo.supportsGyroscope || Application.isEditor; } }

    public float AngleFromUpright() {
        return Vector3.Angle(mainCamera.transform.up, Vector3.up);
    }

    void Start() {
        Input.gyro.enabled = true;
    }

}
