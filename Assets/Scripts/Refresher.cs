using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Refresher : MonoBehaviour {

    public CompassData compassData;
    public TMP_Text labelCompass;
    public TMP_Text labelOriginRotation;

    public LocationData locationData;
    public TMP_Text labelLatitude;
    public TMP_Text labelLongitude;
    public TMP_Text labelAltitude;
    public TMP_Text labelLocationServiceStatus;

    public TMP_Text labelARRecalibrating;

    void Update() {
        PerformRefresh();
    }

    void PerformRefresh() {
        float altitudeInFeet = locationData.currentLocation.altitude * 3.28084f;

        labelLocationServiceStatus.text = "Location Service: " + locationData.locationServiceStatus;
        labelCompass.text = "Compass: " + compassData.lastAvg;
        labelOriginRotation.text = "Origin Rotation: " + compassData.originRotatedAmount + " deg";
        labelLatitude.text = "Latitude: " + locationData.currentLocation.latitude.ToString();
        labelLongitude.text = "Longitude: " + locationData.currentLocation.longitude.ToString();
        labelAltitude.text = "Altitude: " + locationData.currentLocation.altitude.ToString()
                            + " m (" + altitudeInFeet + " ft" + ")";
    }

}
