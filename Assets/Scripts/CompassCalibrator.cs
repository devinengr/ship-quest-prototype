using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassCalibrator : MonoBehaviour {
    
    public Camera mainCamera;
    public CompassData compassData;

    void Update() {
        transform.position = mainCamera.transform.position;
        float camRotYRaw = mainCamera.transform.rotation.eulerAngles.y;
        Quaternion camRotAboutY = Quaternion.Euler(0f, camRotYRaw, 0f);
        Quaternion targetRotation = camRotAboutY * Quaternion.Euler(0f, -compassData.lastAvg, 0f);
        transform.rotation = targetRotation;
    }

}
