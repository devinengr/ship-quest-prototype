using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLogic {

    public static bool Matches(Location l1, Location l2) {
        float delta = 0.00001f;
        if (Mathf.Abs(l1.latitude - l2.latitude) <= delta) {
            if (Mathf.Abs(l1.longitude - l2.longitude) <= delta) {
                if (Mathf.Abs(l1.altitude - l2.altitude) <= delta) {
                    return true;
                }
            }
        } 
        return false;
    }

}
