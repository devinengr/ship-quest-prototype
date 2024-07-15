using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class PlaneCalibratableParent : MonoBehaviour {

    #region Instance Variables

    public PlaneInvisible compassFollowingPlane;
    public Camera mainCamera;
    public string collectableTag;
    public DeviceCompass compassData;

    [Tooltip("Number of milliseconds it takes for object positions to readjust on the first calibration.")]
    public long firstCalibrationTime = 3000;
    [Tooltip("Number of milliseconds before readjusting object positions.")]
    public long recalibrationTime = 10000;
    [Tooltip("Number of rotations to use to calculate average target rotation for calibration.")]
    public int rotationsToAverage = 5;

    public UnityEvent firstCalibrationReady;
    public UnityEvent recalibrationReady;
    public UnityEvent waitingForRecalibrationReady;

    private Quaternion recalibrationRotationInitial;
    private Quaternion recalibrationRotationTarget;
    private int calibrationCount = 0;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    private bool waitingForRecalibrationReadyInvoked = true;

    public int CalibrationCount { get { return calibrationCount; } }
    public long CalibrationTime { get { return elapsedTime; } }

    #endregion

    #region Start

    void Start() {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    #endregion

    #region Update

    private void InvokeWaitingForRecalibrationReady() {
        if (!waitingForRecalibrationReadyInvoked) {
            waitingForRecalibrationReady.Invoke();
            waitingForRecalibrationReadyInvoked = true;
        }
    }

    private void InvokeRecalibrationReady() {
        recalibrationReady.Invoke();
        waitingForRecalibrationReadyInvoked = false;
    }

    private void InvokeFirstCalibrationReady() {
        firstCalibrationReady.Invoke();
        waitingForRecalibrationReadyInvoked = false;
    }

    private void DetachCollectables() {
        List<GameObject> children = new List<GameObject>();
        transform.gameObject.GetChildGameObjects(children);
        foreach (GameObject child in children) {
            if (child.CompareTag(collectableTag)) {
                child.transform.SetParent(null);
            }
        }
    }

    private void AttachCollectables() {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag(collectableTag);
        foreach (GameObject collectable in collectables) {
            collectable.transform.SetParent(transform);
        }
    }

    private float NormalizeElapsedTime(long recalibrationTime) {
        return (float) elapsedTime / recalibrationTime;
    }

    private bool ReadyToCalibrate() {
        bool interludePassed = elapsedTime >= recalibrationTime;
        bool doingFirstCalibration = calibrationCount == 0 && elapsedTime >= firstCalibrationTime;
        if (interludePassed || doingFirstCalibration) {
            if (compassData.Stable) {
                return true;
            } else {
                InvokeWaitingForRecalibrationReady();
            }
        }
        return false;
    }

    void UpdateElapsedTime() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        elapsedTime = currentTime - startTime;
    }

    void ResetElapsedTime() {
        startTime = currentTime;
        UpdateElapsedTime();
    }

    void FollowCameraWithoutMovingCollectables() {
        DetachCollectables();
        transform.position = mainCamera.transform.position;
        AttachCollectables();
    }

    void UpdateRotationTargets() {
        float yOld = compassFollowingPlane.transform.rotation.eulerAngles.y;
        RotationCalculator.AddRotation(yOld, rotationsToAverage);
        float yNew = RotationCalculator.CalcAvgRotation();
        Quaternion rotationTarget = Quaternion.Euler(0f, yNew, 0f);
        recalibrationRotationInitial = transform.rotation;
        recalibrationRotationTarget = rotationTarget;
    }
    
    IEnumerator Calibrate() {
        if (calibrationCount == 1) {
            transform.rotation = recalibrationRotationTarget;
            InvokeFirstCalibrationReady();
        } else {
            InvokeRecalibrationReady();
            while (NormalizeElapsedTime(recalibrationTime) <= 1f) {
                transform.rotation = Quaternion.Slerp(
                    recalibrationRotationInitial,
                    recalibrationRotationTarget,
                    NormalizeElapsedTime(recalibrationTime));
                yield return null;
            }
        }
        
    }

    void LateUpdate() {
        UpdateElapsedTime();
        FollowCameraWithoutMovingCollectables();
        if (ReadyToCalibrate()) {
            calibrationCount++;
            ResetElapsedTime();
            UpdateRotationTargets();
            StartCoroutine(Calibrate());
        }
    }

    #endregion

}
