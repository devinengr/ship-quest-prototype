using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refresher : MonoBehaviour {

    public CompassData compassData;
    public LocationData locationData;
    public GyroData gyroData;

    void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;
        float altitudeInFeet = locationData.currentLocation.altitude * 3.28084f;

        GUILayout.Label("Location Service: " + locationData.locationServiceStatus);
        GUILayout.Label("Compass: " + compassData.lastAvg);
        GUILayout.Label("Gyro: " + gyroData.currentGyro);
        GUILayout.Label("Gyro Yaw: " + gyroData.yaw);
        GUILayout.Label("Latitude: " + locationData.currentLocation.latitude);
        GUILayout.Label("Longitude: " + locationData.currentLocation.longitude);
        GUILayout.Label("Altitude: " + locationData.currentLocation.altitude
                            + " m (" + altitudeInFeet + " ft)");
    }

}
