using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippoSpawner : MonoBehaviour {

    [Tooltip("Locations of notable locations on campus.")]
    public List<Location> locations;
    public GeoConverter converter;
    public GameObject shippoCollectablePrefab;
    public GameObject mainCamera;
    public LocationData locationData;
    public float interactionDistance;
    public GameObject UIShipBall;

    private bool grabbedLocation = false;
    private Location lastPlayerLocation;
    private Dictionary<Location, GameObject> shippoMap;
    private float t;

    void Start() {
        lastPlayerLocation = new Location(0, 0, 0);
        shippoMap = new Dictionary<Location, GameObject>();
        // spawn all Shippos and set their locations
        foreach (Location loc in locations) {
            GameObject shippo = Instantiate(shippoCollectablePrefab);
            Vector3 pos = converter.GeoToCartesian(loc.longitude, loc.altitude, loc.latitude);
            shippo.transform.position = pos;
            shippoMap.Add(loc, shippo);
        }
    }

    void Update() {
        // check if the player moved (or if the app just started)
        if (!grabbedLocation || !lastPlayerLocation.Matches(locationData.currentLocation)) {
            grabbedLocation = true;
            float lat = locationData.currentLocation.latitude;
            float lon = locationData.currentLocation.longitude;
            // update last player location and get Unity coordinates for it
            lastPlayerLocation.latitude = lat;
            lastPlayerLocation.longitude = lon;
            Vector3 playerLocation = converter.GeoToCartesian(lon, 0, lat);
            // adjust all Shippo positions based on new player location
            foreach (Location loc in locations) {
                GameObject shippo = shippoMap[loc];
                Vector3 shippoLoc = converter.GeoToCartesian(loc.longitude, 0, loc.latitude);
                shippoLoc = shippoLoc - playerLocation;
                shippo.transform.position = shippoLoc + mainCamera.transform.position;
            }
        }
        foreach (Location loc in locations) {
            GameObject shippo = shippoMap[loc];
            // change color based on distance
            if (Vector3.Distance(mainCamera.transform.position, shippo.transform.position) < interactionDistance) {
                shippo.GetComponent<Renderer>().material.color = Color.cyan;
            } else {
                shippo.GetComponent<Renderer>().material.color = Color.white;
            }
            // smoothly move Shippo to the ShipBall if grabbed
            if (shippo.GetComponent<ShippoCollectable>().grabbed) {
                shippo.transform.position = Vector3.Lerp(shippo.transform.position, UIShipBall.transform.position, t);
                shippo.transform.localScale = Vector3.Lerp(shippo.transform.localScale, UIShipBall.transform.localScale, t);
                t += 0.2f * Time.deltaTime;
                if (Vector3.Distance(shippo.transform.position, UIShipBall.transform.position) < 0.1f) {
                    shippoMap.Remove(loc);
                    Destroy(shippo.transform.gameObject);
                }
            }
        }
    }

}
