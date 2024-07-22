using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceGyroscope : MonoBehaviour {

    public Camera mainCamera;
    
    public bool DeviceGyroscopeIsEnabled { get {
        return SystemInfo.supportsGyroscope || Application.isEditor; } }

    public float AngleFromPortraitUpright() {
        return mainCamera.transform.rotation.eulerAngles.x;
    }

    public float AngleFromLandscapeUpright() {
        return mainCamera.transform.rotation.eulerAngles.z;
    }

    void Start() {
        Input.gyro.enabled = true;
    }

}
