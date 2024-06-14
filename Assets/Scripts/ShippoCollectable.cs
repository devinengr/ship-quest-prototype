using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippoCollectable : MonoBehaviour {

    public LocationData locationData;
    public GeoConverter converter;

    private Location lastPlayerLocation;
    private bool grabbedLocation = false;

    public Camera camera;
    public float interactionDistance;
    public Location location;

    public bool grabbed { get; set; } = false;
    public GameObject UICubeCollector;
    private float t;

    void Start() {
        lastPlayerLocation = new Location(0, 0, 0);
    }

    void Update() {
        transform.rotation *= Quaternion.Euler(0, 50 * Time.deltaTime, 0);

        if (grabbed) {
            transform.position = Vector3.Lerp(transform.position, UICubeCollector.transform.position, t);
            t += 2 * Time.deltaTime;
            if (transform.position.Equals(UICubeCollector.transform.position)) {
                Destroy(transform.gameObject);
            }
        }

        if (Input.location.status == LocationServiceStatus.Running) {
            if (!grabbedLocation || lastPlayerLocation.latitude != locationData.currentLocation.latitude
                                            || lastPlayerLocation.longitude != locationData.currentLocation.longitude) {
                grabbedLocation = true;
                lastPlayerLocation.latitude = locationData.currentLocation.latitude;
                lastPlayerLocation.longitude = locationData.currentLocation.longitude;

                Vector3 playerLocation = converter.GeoToCartesian(
                    locationData.currentLocation.longitude, 0,
                    locationData.currentLocation.latitude);

                Vector3 objectLocation = converter.GeoToCartesian(
                    location.longitude, 0,
                    location.latitude);

                objectLocation = objectLocation - playerLocation;
                transform.position = objectLocation + camera.transform.position;

                
            }
        }

        if (Vector3.Distance(camera.transform.position, transform.position) < interactionDistance) {
            GetComponent<Renderer>().material.color = Color.cyan;
        } else {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
    }

}
