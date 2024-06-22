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
        // todo more with time

        // temporarily detach the cubes while adjusting the position.
        // this will allow the camera to get closer to the cubes as the
        // player moves toward them.
        // todo implement
        transform.position = mainCamera.transform.position;

        float camRotYRaw = mainCamera.transform.rotation.eulerAngles.y;
        Quaternion camRotAboutY = Quaternion.Euler(0f, camRotYRaw, 0f);

        Quaternion targetRotation = camRotAboutY * Quaternion.Euler(0f, -compassData.lastAvg, 0f);
        transform.rotation = targetRotation;
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothingSpeed);
    }
}
