using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

[Serializable]
public struct Location {

    public string name;
    public float latitude;
    public float longitude;
    public float altitude;

    public Location(string name, float latitude, float longitude, float altitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
        this.altitude = altitude;
    }

    public Location(float latitude, float longitude, float altitude)
                    : this("Unspecified", latitude, longitude, altitude) {}

}

public class LocationData : MonoBehaviour {

    [Tooltip("How often to fetch location data from the device (in milliseconds).")]
    public int updateFrequency;
    
    [Tooltip("The location used if location fetching fails.")]
    public Location defaultLocation;

    [Tooltip("Locations of notable locations on campus.")]
    public List<Location> locations;

    public Location currentLocation { get; set; }
    public LocationServiceStatus locationServiceStatus { get; set; }

    void Start() {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        if (!Input.location.isEnabledByUser) {
            Debug.Log("Location not enabled. Using default latitude and longitude.");
            currentLocation = defaultLocation;
        }
        Input.location.Start(5);
    }

    void Update() {
        locationServiceStatus = Input.location.status;
        GetLocation();
    }

    void GetLocation() {
        if (Input.location.status != LocationServiceStatus.Running) {
            Debug.LogFormat("Unable to fetch device location with status {0}.", Input.location.status);
            return;
        }
        currentLocation = new Location(
            Input.location.lastData.latitude,
            Input.location.lastData.longitude,
            Input.location.lastData.altitude
        );
    }

}
