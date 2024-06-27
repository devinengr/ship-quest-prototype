using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class Refresher : MonoBehaviour {

    public CompassData compassData;
    public LocationData locationData;
    public ARSession sessionAR;
    public ShippoParent shippoParent;

    private bool sessionStateFailedOnce;
    private bool sessionStateWaitingForFailure;
    private bool sessionResetting;

    void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;
        float altitudeInFeet = locationData.currentLocation.altitude * 3.28084f;

        // the app is not traacking when started. if arSession.Reset() is called
        // at this point, the app may crash or be stuck calling Reset() every frame.
        // wait until it reaches the tracking state and then listen for lost tracking.
        if (sessionAR.subsystem.trackingState == TrackingState.Tracking) {
            sessionStateWaitingForFailure = true;
            sessionResetting = false;
        }
        if (sessionStateWaitingForFailure && sessionAR.subsystem.trackingState != TrackingState.Tracking) {
            sessionStateFailedOnce = true;
            if (!sessionResetting) {
                sessionResetting = true;
                sessionAR.Reset();
            }
        }

        GUILayout.Label("Location Service: " + locationData.locationServiceStatus);
        GUILayout.Label("Compass: " + compassData.lastAvg);
        GUILayout.Label("Latitude: " + locationData.currentLocation.latitude);
        GUILayout.Label("Longitude: " + locationData.currentLocation.longitude);
        GUILayout.Label("Altitude: " + locationData.currentLocation.altitude
                            + " m (" + altitudeInFeet + " ft)");
        GUILayout.Label("AR Tracking Status: " + sessionAR.subsystem.trackingState);
        GUILayout.Label("AR Tracking Failed Once: " + sessionStateFailedOnce);
        GUILayout.Label("Compass Calibration Count: " + shippoParent.CalibrationCount);
        GUILayout.Label("Compass Calibration Time: " + (float)shippoParent.CalibrationTime/1000 + " s");
    }

}
