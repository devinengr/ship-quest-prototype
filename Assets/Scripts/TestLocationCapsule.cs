using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocationCapsule : MonoBehaviour {

    public LocationData locationData;
    public GeoConverter converter;

    void Update() {
        Vector3 playerLocation = converter.GeoToCartesian(
            locationData.currentLocation.longitude,
            locationData.currentLocation.altitude,
            locationData.currentLocation.latitude
        );
        Vector3 cubeLocation = converter.GeoToCartesian(
            locationData.defaultLocation.longitude,
            locationData.defaultLocation.altitude,
            locationData.defaultLocation.latitude
        );

        // Vector3 cubeLocation = new Vector3(0, 0, 0);

        cubeLocation = cubeLocation - playerLocation;
        transform.position = cubeLocation;

        Debug.LogFormat("Capsule: {0}, {1}, {2}", transform.position.x, transform.position.y, transform.position.z);
    }

}
