using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;

[RequireComponent(typeof(CalibrationObserverManager))]
public class PlaneCalibratableParent : MonoBehaviour {

    #region Instance Variables

    public PlaneInvisible compassFollowingPlane;
    public Camera mainCamera;
    public string collectableTag;
    public DeviceCompass deviceCompass;
    public DeviceGyroscope deviceGyro;

    public int angleUprightMin = 15;
    public int angleUprightMax = 65;

    [Tooltip("Number of milliseconds to wait to compile compass data before doing first calibration.")]
    public long msToFirstCalibration = 3000;
    [Tooltip("Number of milliseconds before readjusting object positions.")]
    public long msToRecalibration = 10000;
    [Tooltip("Number of rotations to use to calculate average target rotation for calibration.")]
    public int maxRotationsInAverage = 5;
    [Tooltip("Number of milliseconds it takes for object positions to readjust.")]
    public long repositioningPeriod = 1000;

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

    private CalibrationObserverManager calibrationObserverManager;

    #endregion

    #region Start

    void Start() {
        calibrationObserverManager = GetComponent<CalibrationObserverManager>();
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    #endregion

    #region Update

    private void InvokeWaitingForRecalibrationReady() {
        if (!waitingForRecalibrationReadyInvoked) {
            calibrationObserverManager.WaitingForCalibrationReadyHandle();
            waitingForRecalibrationReady.Invoke();
            waitingForRecalibrationReadyInvoked = true;
        }
    }

    private void InvokeRecalibrationReady() {
        calibrationObserverManager.RecalibrationReadyHandle();
        recalibrationReady.Invoke();
        waitingForRecalibrationReadyInvoked = false;
    }

    private void InvokeFirstCalibrationReady() {
        calibrationObserverManager.FirstCalibrationReadyHandle();
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
        bool interludePassed = elapsedTime >= msToRecalibration;
        bool doingFirstCalibration = calibrationCount == 0 && elapsedTime >= msToFirstCalibration;
        if (interludePassed || doingFirstCalibration) {
            float angle = deviceGyro.AngleFromPortraitUpright();
            if (deviceCompass.Stable && angleUprightMin <= angle && angle <= angleUprightMax) {
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
        RotationCalculator.AddRotation(yOld, maxRotationsInAverage);
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
            while (NormalizeElapsedTime(msToRecalibration) <= 1f) {
                transform.rotation = Quaternion.Slerp(
                    recalibrationRotationInitial,
                    recalibrationRotationTarget,
                    NormalizeElapsedTime(repositioningPeriod));
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
