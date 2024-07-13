using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationCollectable : MonoBehaviour {

    public Collectable collectable;

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
        TryInitializePosition();
        name = Loc.Name;
    }

    void Update() {
        TryInitializePosition();

        // todo update over time
    }

}
