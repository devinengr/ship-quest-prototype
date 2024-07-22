using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FadeOnEvent))]
public class FadeInOnCalibratedObserver : MonoBehaviour, ICalibrationObserver {

    public bool searchForManager = false;
    public CalibrationObserverManager observerManager;

    private FadeOnEvent fadeOnEvent;
    private bool calibrated = false;

    public void FirstCalibrationReadyHandle() {
        calibrated = true;
        fadeOnEvent.BeginFade();
    }

    public void RecalibrationReadyHandle() {}

    public void WaitingForCalibrationReadyHandle() {}

    void Start() {
        fadeOnEvent = GetComponent<FadeOnEvent>();
        SharedColorFunctions.SetTransparency(gameObject, 0f);
        if (searchForManager) {
            observerManager = FindAnyObjectByType<CalibrationObserverManager>();
        }
        observerManager.AddObserver(this);
    }

    void LateUpdate() {
        if (!calibrated) {
            SharedColorFunctions.SetTransparency(gameObject, 0f);
        }
    }

}
