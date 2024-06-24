using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShippoParent : MonoBehaviour {

    public GameObject compassCalibrator;
    public Camera mainCamera;
    public CompassData compassData;

    [Tooltip("Number of milliseconds it takes for object positions to readjust on the first calibration.")]
    public long firstCalibrationTime = 3000;
    [Tooltip("Number of milliseconds before readjusting object positions.")]
    public long recalibrationTime = 10000;

    private Quaternion recalibrationRotationInitial;
    private Quaternion recalibrationRotationTarget;
    private int calibrationCount = 0;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    public int CalibrationCount { get { return calibrationCount; } }
    public long CalibrationTime { get { return elapsedTime; } }

    void Start() {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private void DetachHippos() {
        List<GameObject> children = new List<GameObject>();
        transform.gameObject.GetChildGameObjects(children);
        foreach (GameObject child in children) {
            if (child.CompareTag("ShippoTheHippo")) {
                child.transform.SetParent(null);
            }
        }
    }

    private void AttachHippos() {
        GameObject[] hippos = GameObject.FindGameObjectsWithTag("ShippoTheHippo");
        foreach (GameObject hippo in hippos) {
            hippo.transform.SetParent(transform);
        }
    }

    private float NormalizeElapsedTime(long recalibrationTime) {
        return (float) elapsedTime / recalibrationTime;
    }

    void LateUpdate() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        elapsedTime = currentTime - startTime;
        
        // temporarily detach hippo positions from the parent's
        // transform while updating the position of the parent.
        // this will allow the camera to get closer to them as the
        // player moves toward them.
        DetachHippos();
        transform.position = mainCamera.transform.position;
        AttachHippos();

        // after a number of seconds, readjust the rotation of the parent
        // of the hippos. this will readjust the position of the hippos
        // according to new compass data. wait a few seconds before
        // starting the first calibration to give the calibrator object
        // time to rotate to the correct orientation.
        if (elapsedTime >= recalibrationTime || (calibrationCount == 0 && elapsedTime >= firstCalibrationTime)) {
            // check if the compass average is stable before readjusting.
            if (compassData.stable) {
                startTime = currentTime;
                recalibrationRotationInitial = transform.rotation;

                // average all rotation targets from when the app started
                // to get the cubes to be in a more accurate position over
                // time.
                RotationCalculator.AddRotation(compassCalibrator.transform.rotation.eulerAngles);
                recalibrationRotationTarget = RotationCalculator.CalcAvgRotation();

                calibrationCount += 1;
            }
        }

        if (calibrationCount >= 1) {
            // rotate the parent faster on the first pass. this allows
            // subsequent rotations to be much slower to make them less
            // noticeable to the user (without sacrificing the initial
            // rotation).
            if (calibrationCount == 1) {
                transform.rotation = recalibrationRotationTarget;
            } else {
                transform.rotation = Quaternion.Slerp(
                    recalibrationRotationInitial,
                    recalibrationRotationTarget,
                    NormalizeElapsedTime(recalibrationTime));
            }
        }
    }

}
