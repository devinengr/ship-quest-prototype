using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refresher : MonoBehaviour {

    public CompassData compassData;
    public TMP_Text labelCompass;

    public LocationData locationData;
    public TMP_Text labelLatitude;
    public TMP_Text labelLongitude;
    public TMP_Text labelAltitude;
    public TMP_Text labelLocationServiceStatus;

    private int lastUpdateTime = 0;
    private bool hasBeenUpdated = false;

    void Update() {
        int currentTime = Environment.TickCount;
        PerformRefresh();
    }

    void PerformRefresh() {
        labelCompass.text = "Compass: " + compassData.lastAvg;
        labelLatitude.text = "Latitude: " + locationData.currentLocation.latitude.ToString();
        labelLongitude.text = "Longitude: " + locationData.currentLocation.longitude.ToString();
        labelAltitude.text = "Altitude: ...";
        labelLocationServiceStatus.text = "Location Service: " + locationData.locationServiceStatus;
    }

}
