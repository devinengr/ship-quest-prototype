using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowOnEventIfCalibratedObserver : UIShowOnEvent, ICalibrationObserver {

    public bool searchForManager = false;
    public CalibrationObserverManager observerManager;

    private bool calibrated = false;

    public override void ShowOnEvent() {
        if (calibrated) {
            toShow.SetActive(true);
        }
    }

    public void FirstCalibrationReadyHandle() {
        calibrated = true;
    }

    public void RecalibrationReadyHandle() {}

    public void WaitingForCalibrationReadyHandle() {}

    void Start() {
        if (searchForManager) {
            observerManager = FindAnyObjectByType<CalibrationObserverManager>();
        }
        observerManager.AddObserver(this);
    }

}
