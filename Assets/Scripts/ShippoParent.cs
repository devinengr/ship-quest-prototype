using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippoParent : MonoBehaviour {

    public GameObject compassCalibrator;

    [Tooltip("How many seconds before readjusting object positions.")]
    public long recalibrationInterval;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    void Start() {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
    }

    void LateUpdate() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        elapsedTime = currentTime - startTime;

        // after a number of seconds, readjust the rotation of the parent
        // of the hippos. this will readjust the position of the hippos
        // according to new compass data.
        if (elapsedTime >= recalibrationInterval) {
            startTime = currentTime;
            transform.rotation = compassCalibrator.transform.rotation;
        }
    }

}
