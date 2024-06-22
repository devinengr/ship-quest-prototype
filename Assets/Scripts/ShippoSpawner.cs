using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShippoSpawner : MonoBehaviour {

    [Tooltip("Locations of notable locations on campus.")]
    public List<Location> locations;
    public GameObject shippoCollectablePrefab;
    public GameObject mainCamera;
    public LocationData locationData;
    public float interactionDistance;
    public GameObject UIShipBall;

    public GameObject cameraLocCopy;

    public Dictionary<Location, GameObject> shippoMap { get; set; }
    private bool grabbedLocation = false;
    private Location lastPlayerLocation;
    private float t;

    void Start() {
        // initialize the origin location
        GPSEncoder.SetLocalOrigin(new Vector2(
            locationData.currentLocation.latitude,
            locationData.currentLocation.longitude));
        // initialize variables
        lastPlayerLocation = new Location(0, 0, 0);
        shippoMap = new Dictionary<Location, GameObject>();
        // spawn all Shippos and set their locations
        foreach (Location loc in locations) {
            GameObject shippo = Instantiate(shippoCollectablePrefab);
            Vector3 pos = GPSEncoder.GPSToUCS(loc.latitude, loc.longitude);
            // pos.Scale(new Vector3(0.0001f, 0.0001f, 0.0001f));
            Debug.Log(loc.name + " " + pos);
            shippo.transform.position = pos;
            Debug.Log(loc.name + " " + shippo.transform.position);
            shippo.transform.SetParent(cameraLocCopy.transform);
            shippoMap.Add(loc, shippo);
            NameShippoLabel(shippo, loc.name);
        }
    }

    void NameShippoLabel(GameObject shippo, string name) {
        GameObject child = shippo.transform.GetChild(0).gameObject;
        child.GetComponent<TMP_Text>().text = name;
    }

    void MakeShippoLabelFaceCamera(GameObject shippo) {
        GameObject child = shippo.transform.GetChild(0).gameObject;
        child.transform.LookAt(mainCamera.transform);
        child.transform.Rotate(new Vector3(0, 180, 0));
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
            GPSEncoder.SetLocalOrigin(new Vector2(lat, lon));
            // adjust all Shippo positions based on new player location
            foreach (Location loc in locations) {
                GameObject shippo = shippoMap[loc];
                // don't readjust the location of the Shippo if it's already grabbed
                // because it's currently moving to the ShipBall.
                if (!shippo.GetComponent<ShippoCollectable>().grabbed) {
                    shippo.transform.position = GPSEncoder.GPSToUCS(loc.latitude, loc.longitude);
                }
            }
        }
        // loop through all Shippos. if any need to be removed, queue them
        // for deletion after all Shippos have been iterated through
        List<Location> toRemove = new List<Location>();
        foreach (KeyValuePair<Location, GameObject> pair in shippoMap) {
            Location loc = pair.Key;
            GameObject shippo = pair.Value;
            // make the label face the main camera
            MakeShippoLabelFaceCamera(shippo);
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
                t += 0.5f * Time.deltaTime;
                if (Vector3.Distance(shippo.transform.position, UIShipBall.transform.position) < 0.1f) {
                    toRemove.Add(loc);
                }
            }
        }
        // delete queued Shippos
        foreach (Location loc in toRemove) {
            Destroy(shippoMap[loc].transform.gameObject);
            shippoMap.Remove(loc);
        }
    }

}
