using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICalibrationObserver {

    void FirstCalibrationReadyHandle();
    void RecalibrationReadyHandle();
    void WaitingForCalibrationReadyHandle();

}
