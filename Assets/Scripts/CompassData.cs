using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CompassData : MonoBehaviour {

    [Range(5, 30)]
    public int maxCompassInitChecks;

    public GameObject worldOrigin;

    private int compassIter = 0;
    private float[] lastCompassReads;
    private bool compassStarted = false;
    private float lastAdjustedReading = 0;
    public float lastAvg { get; set; } = 0;

    private bool firstAvgObtained = false;
    private bool secondAvgObtained = false;

    private bool originAnchored = false;
    public float originRotatedAmount { get; set; } = 0;

    void Start() {
        // Enable compass
        Input.gyro.enabled = true;
        Input.compass.enabled = true;

        // Initialize
        lastCompassReads = new float[maxCompassInitChecks];
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
            AnchorSessionOrigin();
        }

        // todo temp
        Vector3 pos = worldOrigin.transform.position;
        Debug.LogFormat("World origin: {0}, {1}, {2}", pos.x, pos.y, pos.z);
        // end todo temp
    }

    public void AnchorSessionOrigin() {
        // Rotation of the parent transform (AR Session Origin) will rotate the
        // camera successfully. Call it once after the averages come in.
        // It will anchor the "origin camera"
        // to the correct position, which anchors all airports and airplanes to
        // the correct world orientation position. Only do this once.
        //
        // The compass may need a small amount of time to initialize. Wait until
        // the last compass reading is not the default value.

        if (!originAnchored && secondAvgObtained) {
            worldOrigin.transform.rotation = Quaternion.Euler(new Vector3(0f, lastAvg, 0f));
            originAnchored = true;
            originRotatedAmount = lastAvg;

            for (int i = 0; i < lastCompassReads.Length; i++) {
                Debug.Log("value: " + lastCompassReads[i]);
            }
        }
    }

    private void UpdateCompassList() {
        // Update the compass readings list
        float newReading = Input.compass.trueHeading;
        lastAdjustedReading = CompassLogic.AdjustNewReading(lastAdjustedReading, newReading);
        lastCompassReads[compassIter] = lastAdjustedReading;
        compassIter += 1;
        // If compassIter is too large, reset it.
        // Wait until after the second pass before being confident about the average's accuracy.
        if (compassIter >= maxCompassInitChecks) {
            compassIter = 0;
            if (firstAvgObtained) {
                secondAvgObtained = true;
            }
            firstAvgObtained = true;
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
