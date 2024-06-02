using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

[Serializable]
public struct Location {

    public string name;
    public double latitude;
    public double longitude;

    public Location(string name, double latitude, double longitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public Location(double latitude, double longitude) {
        this.name = "Unspecified";
        this.latitude = latitude;
        this.longitude = longitude;
    }

}

public class LocationData : MonoBehaviour {

    [Tooltip("How often to fetch location data from the device (in milliseconds).")]
    public int updateFrequency;
    
    [Tooltip("The location used if location fetching fails.")]
    public Location defaultLocation;

    [Tooltip("Locations of notable locations on campus.")]
    public List<Location> locations;

    public Location currentLocation { get; set; }
    public LocationServiceStatus locationServiceStatus { get; }

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        if (!Input.location.isEnabledByUser) {
            Debug.Log("Location not enabled. Using default latitude and longitude.");
            currentLocation = defaultLocation;
        }
        Input.location.Start();
    }

    private int lastUpdateTime = 0;
    private bool hasBeenUpdated = false;

    void Update() {
        int currentTime = Environment.TickCount;
        if (!hasBeenUpdated || currentTime - lastUpdateTime >= updateFrequency) {
            GetLocation();
            hasBeenUpdated = true;
            lastUpdateTime = Environment.TickCount;
        }
    }

    void GetLocation() {
        if (Input.location.status != LocationServiceStatus.Running) {
            Debug.LogFormat("Unable to fetch device location with status {0}.", Input.location.status);
            return;
        }
        currentLocation = new Location(Input.location.lastData.latitude, Input.location.lastData.longitude);
    }

}
