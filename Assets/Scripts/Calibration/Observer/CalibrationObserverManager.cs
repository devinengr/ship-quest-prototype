using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationObserverManager : MonoBehaviour {

    private readonly List<ICalibrationObserver> observers = new();

    public void AddObserver(ICalibrationObserver observer) {
        observers.Add(observer);
    }

    public void FirstCalibrationReadyHandle() {
        foreach (ICalibrationObserver observer in observers) {
            observer.FirstCalibrationReadyHandle();
        }
    }

    public void RecalibrationReadyHandle() {
        foreach (ICalibrationObserver observer in observers) {
            observer.RecalibrationReadyHandle();
        }
    }

    public void WaitingForCalibrationReadyHandle() {
        foreach (ICalibrationObserver observer in observers) {
            observer.WaitingForCalibrationReadyHandle();
        }
    }

}
