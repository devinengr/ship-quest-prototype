using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CompassData : MonoBehaviour {

    [Range(5, 90)]
    public int compassAverageCount;
    [Range(0.01f, 1f)]
    public float smoothingSpeed;

    public GameObject worldOrigin;
    public Camera mainCamera;
    public GameObject cameraLocCopy;
    public ShippoSpawner shippoSpawner;

    private int compassIter = 0;
    private float[] lastCompassReads;
    private bool compassStarted = false;
    private float lastAdjustedReading = 0;
    public float lastAvg { get; set; } = 0;

    void Start() {
        // Enable compass
        Input.gyro.enabled = true;
        Input.compass.enabled = true;

        // Initialize
        lastCompassReads = new float[compassAverageCount];
    }

    void Update() {
        // Wait for the compass to start returning valid values. Before it
        // starts, it just returns 0. Even if the user precisely pointed their
        // phone north, the compass wouldn't return exactly 0, so check for when it's not 0.
        if (!compassStarted) {
            float reading = Input.compass.trueHeading;
            Debug.Log(reading);
            // Floating-point comparison includes delta due to possible inaccuracy
            if (reading - 0f >= 0.000001) {
                compassStarted = true;
            }
        }

        if (compassStarted) {
            UpdateCompassList();
            UpdateCompassAverage();
        }

        // todo move to new script dedicated for cameraLocCopy
        cameraLocCopy.transform.position = mainCamera.transform.position;

        float camRotYRaw = mainCamera.transform.rotation.eulerAngles.y;
        Quaternion camRotAboutY = Quaternion.Euler(0f, camRotYRaw, 0f);

        Quaternion targetRotation = camRotAboutY * Quaternion.Euler(0f, -lastAvg, 0f);
        cameraLocCopy.transform.rotation = Quaternion.Slerp(cameraLocCopy.transform.rotation, targetRotation, smoothingSpeed);
    }

    private void UpdateCompassList() {
        // Update the compass readings list
        float newReading = Input.compass.trueHeading;
        lastAdjustedReading = CompassLogic.AdjustNewReading(lastAdjustedReading, newReading);
        lastCompassReads[compassIter] = lastAdjustedReading;
        compassIter += 1;
        // If compassIter is too large, reset it.
        // Wait until after the second pass before being confident about the average's accuracy.
        if (compassIter >= compassAverageCount) {
            compassIter = 0;
        }
    }

    private void UpdateCompassAverage() {
        // Get the average compass reading
        float sum = 0;
        for (int i = 0; i < lastCompassReads.Length; i++) {
            sum += lastCompassReads[i];
        }
        lastAvg = sum / lastCompassReads.Length % 360;
    }

}
