using System.Collections.Generic;
using UnityEngine;

public class GPSEncoderUtil {

    private static Location current;
    private static Dictionary<GameObject, Location> usedNewLocation = new Dictionary<GameObject, Location>();

    public static bool OriginInitComplete { get; set; }

    public static bool IsReadyToInitOrigin() {
        return Input.location.status == LocationServiceStatus.Running ||
                    Application.isEditor;
    }

    public static bool OriginIsUpToDate(Location currentNew) {
        return LocationLogic.CompareLatLon(current, currentNew);
    }

    public static void UpdateOrigin(Location newLoc) {
        GPSEncoder.SetLocalOrigin(newLoc.LatLonVector);
        current = newLoc;
    }

    public static bool ObjectUsedNewLocationOrigin(GameObject obj) {
        if (usedNewLocation.ContainsKey(obj)) {
            return LocationLogic.CompareLatLon(current, usedNewLocation[obj]);
        }
        return false;
    }

    public static void UpdateObjectLocationOrigin(GameObject obj) {
        if (usedNewLocation.ContainsKey(obj)) {
            usedNewLocation.Remove(obj);
        }
        usedNewLocation.Add(obj, current);
    }

}
