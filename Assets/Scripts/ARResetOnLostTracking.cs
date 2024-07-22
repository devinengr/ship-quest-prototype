using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ARResetOnLostTracking : MonoBehaviour {
    
    public ARSession sessionAR;
    private bool sessionStateWaitingForFailure;
    private bool sessionResetting;

    void Update() {
        // the app is not traacking when started. if arSession.Reset() is called
        // at this point, the app may crash or be stuck calling Reset() every frame.
        // wait until it reaches the tracking state and then listen for lost tracking.
        if (sessionAR.subsystem.trackingState == TrackingState.Tracking) {
            sessionStateWaitingForFailure = true;
            sessionResetting = false;
        }
        if (sessionStateWaitingForFailure && sessionAR.subsystem.trackingState != TrackingState.Tracking) {
            if (!sessionResetting) {
                sessionResetting = true;
                sessionAR.Reset();
            }
        }
    }

}
