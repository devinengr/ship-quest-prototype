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
    [Tooltip("Number of rotations to use to calculate average target rotation for calibration.")]
    public int rotationsToAverage = 5;

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

    private bool ReadyToCalibrate() {
        bool interludePassed = elapsedTime >= recalibrationTime;
        bool doingFirstCalibration = calibrationCount == 0 && elapsedTime >= firstCalibrationTime;
        if (interludePassed || doingFirstCalibration) {
            if (compassData.stable) {
                return true;
            }
        }
        return false;
    }

    void UpdateElapsedTime() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        elapsedTime = currentTime - startTime;
    }

    void FollowCameraWithoutMovingHippos() {
        DetachHippos();
        transform.position = mainCamera.transform.position;
        AttachHippos();
    }

    void UpdateRotationTargets() {
        float yOld = compassCalibrator.transform.rotation.eulerAngles.y;
        RotationCalculator.AddRotation(yOld, rotationsToAverage);
        float yNew = RotationCalculator.CalcAvgRotation();
        Quaternion rotationTarget = Quaternion.Euler(0f, yNew, 0f);
        recalibrationRotationInitial = transform.rotation;
        recalibrationRotationTarget = rotationTarget;
    }
    
    void Calibrate() {
        if (calibrationCount == 1) {
            transform.rotation = recalibrationRotationTarget;
        } else {
            transform.rotation = Quaternion.Slerp(
                recalibrationRotationInitial,
                recalibrationRotationTarget,
                NormalizeElapsedTime(recalibrationTime));
        }
    }

    void LateUpdate() {
        UpdateElapsedTime();
        FollowCameraWithoutMovingHippos();
        // todo move the following code into a coroutine
        if (ReadyToCalibrate()) {
            startTime = currentTime;
            UpdateRotationTargets();        
            calibrationCount += 1;
        }
        if (calibrationCount >= 1) {
            Calibrate();
        }
    }

}
