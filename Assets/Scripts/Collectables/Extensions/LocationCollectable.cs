using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCollectable : MonoBehaviour {

    public Collectable collectable;
    public Location location;

    void Start() {
        Vector3 ucs = GPSEncoder.GPSToUCS(location.LatLonVector);
        transform.position = ucs;
    }

    void Update() {
        // todo update over time
    }

}
