using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationCollectable : MonoBehaviour {

    public Collectable collectable;

    public bool searchForLocObject = false;
    public DeviceLocation deviceLocation;

    public Location Loc { get; set; }
    
    private bool initializedPosition = false;

    void TryInitializePosition() {
        if (!initializedPosition) {
            if (LocationLogic.LocationIsInitialized) {
                transform.position = GPSEncoder.GPSToUCS(Loc.LatLonVector);
                initializedPosition = true;
            }
        }
    }

    void Start() {
        if (searchForLocObject) {
            deviceLocation = FindObjectOfType<DeviceLocation>();
        }
        TryInitializePosition();
        name = Loc.Name;
    }

    void Update() {
        TryInitializePosition();
        if (deviceLocation.ReceivedNewGPSInfoLastFrame) {
            transform.position = GPSEncoder.GPSToUCS(Loc.LatLonVector);
        }
    }

}
