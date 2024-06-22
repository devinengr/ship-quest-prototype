using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassCalibrator : MonoBehaviour {
    
    public Camera mainCamera;
    public CompassData compassData;

    [Tooltip("How many seconds it takes for objects to be readjusted by half the distance they normally would.")]
    public long secondsToHalfAdjustmentStrength;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    void Start() {
        startTime = Environment.TickCount / TimeSpan.TicksPerSecond;
    }

    void Update() {
        currentTime = Environment.TickCount / TimeSpan.TicksPerSecond;
        elapsedTime = currentTime - startTime;
        // todo do more with timing stuff
        // specifically, make the rotation of this object transform
        // less inclined to rotate as more time passes.

        transform.position = mainCamera.transform.position;

        float camRotYRaw = mainCamera.transform.rotation.eulerAngles.y;
        Quaternion camRotAboutY = Quaternion.Euler(0f, camRotYRaw, 0f);

        Quaternion targetRotation = camRotAboutY * Quaternion.Euler(0f, -compassData.lastAvg, 0f);
        transform.rotation = targetRotation;

    }
}
