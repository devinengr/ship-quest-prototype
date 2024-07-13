using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {

    public List<Location> locations;
    public LocationCollectable collectablePrefab;
    public GameObject collectableParent;

    void Start() {
        foreach (Location location in locations) {
            LocationCollectable newPrefab = Instantiate(collectablePrefab);
            newPrefab.Loc = location;
            newPrefab.transform.SetParent(collectableParent.transform);
        }
    }

}
