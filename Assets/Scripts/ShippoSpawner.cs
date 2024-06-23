using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShippoSpawner : MonoBehaviour {

    #region Instance Variables

    [Tooltip("Locations of notable locations on campus.")]
    public List<Location> locations;
    public GameObject shippoCollectablePrefab;
    public GameObject mainCamera;
    public LocationData locationData;
    public float interactionDistance = 50;
    public GameObject UIShipBall;

    public GameObject shippoParent;

    public Dictionary<Location, GameObject> shippoMap { get; set; }
    private Location lastPlayerLocation;

    // todo this value is never reset and doesn't account for when multiple cubes
    // are grabbed at the same time
    private float t;

    #endregion

    #region Start

    void SetGPSEncoderOriginAsCurrentLocation() {
        GPSEncoder.SetLocalOrigin(new Vector2(
            locationData.currentLocation.latitude,
            locationData.currentLocation.longitude));
    }

    void NameShippoLabel(GameObject shippo, string name) {
        GameObject child = shippo.transform.GetChild(0).gameObject;
        child.GetComponent<TMP_Text>().text = name;
    }

    void SpawnAllShippos() {
        foreach (Location loc in locations) {
            GameObject shippo = Instantiate(shippoCollectablePrefab);
            shippo.transform.SetParent(shippoParent.transform, false);
            shippo.transform.localPosition = GPSEncoder.GPSToUCS(loc.latitude, loc.longitude);
            shippoMap.Add(loc, shippo);
            NameShippoLabel(shippo, loc.name);
        }
    }

    void InitializeVariables() {
        lastPlayerLocation = new Location(0, 0, 0);
        shippoMap = new Dictionary<Location, GameObject>();
    }

    void Start() {
        SetGPSEncoderOriginAsCurrentLocation();
        InitializeVariables();
        SpawnAllShippos();
    }

    #endregion

    #region Update

    void MakeShippoLabelFaceCamera(GameObject shippo) {
        GameObject child = shippo.transform.GetChild(0).gameObject;
        child.transform.LookAt(mainCamera.transform);
        child.transform.Rotate(new Vector3(0, 180, 0));
    }

    void SetLastPlayerLocationAsCurrent() {
        lastPlayerLocation.latitude = locationData.currentLocation.latitude;
        lastPlayerLocation.longitude = locationData.currentLocation.longitude;
    }

    bool ReceivedNewGPSLocationInfo() {
        return !LocationLogic.Matches(lastPlayerLocation, locationData.currentLocation);
    }

    void RepositionNonGrabbedShippos() {
        foreach (Location loc in shippoMap.Keys) {
            GameObject shippo = shippoMap[loc];
            if (!shippo.GetComponent<ShippoCollectable>().grabbed) {
                shippo.transform.localPosition = GPSEncoder.GPSToUCS(loc.latitude, loc.longitude);
            }
        }
    }

    void DeleteShipposByLocation(List<Location> toRemove) {
        foreach (Location loc in toRemove) {
            Destroy(shippoMap[loc].transform.gameObject);
            shippoMap.Remove(loc);
        }
    }

    void UpdateShippoColorByDistance(GameObject shippo) {
        if (Vector3.Distance(mainCamera.transform.position, shippo.transform.position) < interactionDistance) {
            shippo.GetComponent<Renderer>().material.color = Color.cyan;
        } else {
            shippo.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    bool ShippoIsGrabbed(GameObject shippo) {
        return shippo.GetComponent<ShippoCollectable>().grabbed;
    }

    void MoveShippoTowardBall(GameObject shippo) {
        shippo.transform.position = Vector3.Lerp(shippo.transform.position, UIShipBall.transform.position, t);
        shippo.transform.localScale = Vector3.Lerp(shippo.transform.localScale, UIShipBall.transform.localScale, t);
        t += 0.5f * Time.deltaTime;
    }

    bool ShippoIsCloseToBall(GameObject shippo) {
       return Vector3.Distance(shippo.transform.position, UIShipBall.transform.position) < 0.1f; 
    }

    void UpdateShippos() {
        List<Location> toRemove = new();
        foreach (Location loc in shippoMap.Keys) {
            GameObject shippo = shippoMap[loc];
            MakeShippoLabelFaceCamera(shippo);
            UpdateShippoColorByDistance(shippo);
            if (ShippoIsGrabbed(shippo)) {
                MoveShippoTowardBall(shippo);
                if (ShippoIsCloseToBall(shippo)) {
                    toRemove.Add(loc);
                }
            }
        }
        DeleteShipposByLocation(toRemove);
    }

    void Update() {
        if (ReceivedNewGPSLocationInfo()) {
            SetLastPlayerLocationAsCurrent();
            SetGPSEncoderOriginAsCurrentLocation();
            RepositionNonGrabbedShippos();
        }
        UpdateShippos();
    }

    #endregion

}
