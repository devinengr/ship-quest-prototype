using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

public class LocationData : MonoBehaviour {

    [Tooltip("How often to fetch location data from the device (in milliseconds).")]
    public int updateFrequency;
    
    [Tooltip("The location used if location fetching fails.")]
    public Location defaultLocation;

    public Location currentLocation { get; private set; }
    public LocationServiceStatus locationServiceStatus { get; private set; }

    void Start() {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        if (!Input.location.isEnabledByUser) {
            Debug.Log("Location not enabled. Using default latitude and longitude.");
            currentLocation = defaultLocation;
        }
        Input.location.Start();
    }

    void Update() {
        locationServiceStatus = Input.location.status;
        GetLocation();
    }

    void GetLocation() {
        if (!Application.isEditor) {
            if (Input.location.status != LocationServiceStatus.Running) {
                Debug.LogFormat("Unable to fetch device location with status {0}.", Input.location.status);
                return;
            }
            currentLocation = new Location(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude,
                Input.location.lastData.altitude);
        }
    }

}
