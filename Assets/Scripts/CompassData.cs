using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CompassData : MonoBehaviour {

    [Range(5, 90)]
    public int compassAverageCount;

    [Tooltip("The number of degrees that the last compass averages can differ by to be considered stable.")]
    public int degreeRangeForStability;

    public GameObject worldOrigin;
    public Camera mainCamera;
    public ShippoSpawner shippoSpawner;

    private bool compassStarted = false;

    private int compassIter = 0;
    private float[] lastCompassReads;
    private float lastAdjustedReading = 0;

    private int avgIter = 0;
    private float[] lastCompassAverages;
    public float lastAvg { get; private set; } = 0;

    public bool stable { get; private set; } = false;

    void Start() {
        // Enable compass
        Input.gyro.enabled = true;
        Input.compass.enabled = true;

        // Initialize
        lastCompassReads = new float[compassAverageCount];
        lastCompassAverages = new float[compassAverageCount];
    }

    void Update() {
        // Wait for the compass to start returning valid values. Before it
        // starts, it just returns 0. Even if the user precisely pointed their
        // phone north, the compass wouldn't return exactly 0, so check for when it's not 0.
        if (!compassStarted) {
            float reading = Input.compass.trueHeading;
            // Floating-point comparison includes delta due to possible inaccuracy
            if (reading - 0f >= 0.000001) {
                compassStarted = true;
            }
        }

        if (compassStarted) {
            UpdateCompassList();
            UpdateCompassAverage();
            UpdateAverageList();
            DetermineStability();
        }
    }

    private void DetermineStability() {
        // turning the camera means the compass average will lag behind for a second, so
        // combining the two will cause hippos to be placed incorrectly. to counter this,
        // wait for the duration that the compass is updated, and as long as all averages
        // are similar by up to a number of degrees, update it. this works because the
        // averages are typically very smooth.
        float min = Mathf.Min(lastCompassAverages);
        float max = Mathf.Max(lastCompassAverages);
        stable = max - min <= degreeRangeForStability;
    }

    private void UpdateCompassList() {
        // update the compass readings list
        float newReading = Input.compass.trueHeading;
        lastAdjustedReading = CompassLogic.AdjustNewReading(lastAdjustedReading, newReading);
        lastCompassReads[compassIter] = lastAdjustedReading;
        compassIter += 1;
        // if compassIter is too large, reset it.
        if (compassIter >= compassAverageCount) {
            compassIter = 0;
        }
    }

    private void UpdateCompassAverage() {
        // get the average compass reading
        float sum = 0;
        for (int i = 0; i < lastCompassReads.Length; i++) {
            sum += lastCompassReads[i];
        }
        lastAvg = sum / lastCompassReads.Length % 360;
    }

    private void UpdateAverageList() {
        lastCompassAverages[avgIter] = lastAvg;
        avgIter += 1;
        // if avgIter is too large, reset it.
        if (avgIter >= compassAverageCount) {
            avgIter = 0;
        }
    }

}
