using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLogic {

    public static bool CompareLatLon(Location l1, Location l2) {
        float delta = 0.00001f;
        if (Mathf.Abs(l1.latitude - l2.latitude) <= delta) {
            if (Mathf.Abs(l1.longitude - l2.longitude) <= delta) {
                return true;
            }
        } 
        return false;
    }

}
