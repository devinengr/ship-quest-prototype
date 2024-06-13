using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoConverter : MonoBehaviour {

    public float DegToRad(float deg) {
        return deg * Mathf.Deg2Rad;
    }

    public float RadToDeg(float rad) {
        return rad * Mathf.Rad2Deg;
    }

    // public Vector3 GeoToCartesian(float longitude, float altitude, float latitude) {
    //     float earthRadiusInFeet = 3958.8f * 5280f;
    //     float longRad = DegToRad(longitude);
    //     float latRad = DegToRad(longitude);

    //     float x = earthRadiusInFeet * Mathf.Cos(DegToRad(latitude)) * Mathf.Co
    // }
    
    public Vector3 GeoToCartesian(float longitude, float altitude, float latitude) {
        // x = longitude
        // y = altitude
        // z = latitude
        // 
        // Latitude value differs as you get closer to the North Pole.
        // The compass orients the scene such that the objects are anchored
        // about the North Pole (0/360). So, when facing the phone toward
        // the North Pole, depth represents latitude. So, we will use
        // z for latitude, x for longitude.

        // There are 69.172 miles in one degree of latitude.
        // Longitude varies. Closer to the North Pole, one longitude
        // degree is much shorter than if it is closer to the Equator.
        // Calculate it, and be sure to use DEG mode.
        float feetPerMile = 5280.0f;
        float milesPerLatitude = 69.172f;
        float milesPerLongitude = Mathf.Cos(DegToRad((float) latitude)) * milesPerLatitude;

        // Miles to Feet
        float feetPerAltitude = 1;
        float feetPerLatitude = milesPerLatitude * feetPerMile;
        float feetPerLongitude = milesPerLongitude * feetPerMile;

        // Feet to meters
        float feetPerMeter = 3.280839f;
        float metersPerAltitude = feetPerAltitude / feetPerMeter;
        float metersPerLatitude = feetPerLatitude / feetPerMeter;
        float metersPerLongitude = feetPerLongitude / feetPerMeter;

        // Meters to Vector
        float x = longitude * metersPerLongitude;
        float y = altitude * metersPerAltitude;
        float z = latitude * metersPerLatitude;

        // Adjust scaling to accurately match the object position in real life.
        // Distance traveled IRL roughly equals 63% of distance traveled in Unity.
        // If the hypotenuse gains 63% of itself, X and Z gain 63% of themselves
        // as well.
        // x /= 0.63f;
        // z /= 0.63f; todo unity actually didn't respond to this

        return new Vector3(x, y, z);
    }

}
