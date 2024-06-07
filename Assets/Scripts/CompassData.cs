using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassData : MonoBehaviour {

    [Range(5, 30)]
    public int maxCompassInitChecks;

    public GameObject worldOrigin;

    private int compassIter = 0;
    private float[] lastCompassReads;

    private float lastAdjustedReading = 0;
    public float lastAvg { get; set; }

    // private bool originAnchored = false;
    // private float originRotatedAmount = 0;

    void Start() {
        // Enable compass
        Input.gyro.enabled = true;
        Input.compass.enabled = true;

        // Initialize
        lastCompassReads = new float[maxCompassInitChecks];
    }

    void Update() {
        UpdateCompassList();
        UpdateCompassAverage();
        // AnchorSessionOrigin();

        // allow chaos to occur
        // worldOrigin.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);
    }

    // private void AnchorSessionOrigin() {
    //     // Rotation of the parent transform (AR Session Origin) will rotate the
    //     // camera successfully. Call it once after the averages come in.
    //     // It will anchor the "origin camera"
    //     // to the correct position, which anchors all airports and airplanes to
    //     // the correct world orientation position. Only do this once.
    //     //
    //     // The compass may need a small amount of time to initialize. Wait until
    //     // the last compass reading is not the default value.
    //     if (!originAnchored && lastRaw != 0) {
    //         transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 180 + lastRaw, 0f));
    //         originAnchored = true;
    //         // originRotatedAmount = lastRaw;
    //     }
    // }

    private void UpdateCompassList() {
        // Update the compass readings list
        float newReading = Input.compass.trueHeading;
        lastAdjustedReading = CompassLogic.AdjustNewReading(lastAdjustedReading, newReading);
        lastCompassReads[compassIter % maxCompassInitChecks] = lastAdjustedReading;
        compassIter += 1;
        // If compassIter is too large, reset it
        if (compassIter >= int.MaxValue) {
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
