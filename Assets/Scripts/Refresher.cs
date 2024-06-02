using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refresher : MonoBehaviour {

    [Range(1,60000)]
    [Tooltip("How often the refresher will update the UI elements (in milliseconds).")]
    public int updateFrequency;

    public RotationData rotationData;
    public TMP_Text labelRotation;

    public LocationData locationData;
    public TMP_Text labelLatitude;
    public TMP_Text labelLongitude;
    public TMP_Text labelAltitude;
    public TMP_Text labelLocationServiceStatus;

    private int lastUpdateTime = 0;
    private bool hasBeenUpdated = false;

    void Update() {
        int currentTime = Environment.TickCount;
        if (!hasBeenUpdated || currentTime - lastUpdateTime >= updateFrequency) {
            PerformRefresh();
            hasBeenUpdated = true;
            lastUpdateTime = Environment.TickCount;
        }
    }

    void PerformRefresh() {
        labelRotation.text = "Rotation: ...";
        labelLatitude.text = "Latitude: " + locationData.currentLocation.latitude.ToString();
        labelLongitude.text = "Longitude: " + locationData.currentLocation.longitude.ToString();
        labelAltitude.text = "Altitude: ...";
        labelLocationServiceStatus.text = "Location Service: " + locationData.locationServiceStatus;
    }

}
