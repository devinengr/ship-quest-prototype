using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLogic {

    public static bool CompareLatLon(Location l1, Location l2) {
        float delta = 0.00001f;
        if (Mathf.Abs(l1.Latitude - l2.Latitude) <= delta) {
            if (Mathf.Abs(l1.Longitude - l2.Longitude) <= delta) {
                return true;
            }
        } 
        return false;
    }

}
