using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Location {

    [SerializeField]
    private string name;
    [SerializeField]
    private float latitude;
    [SerializeField]
    private float longitude;
    [SerializeField]
    private float altitude;

    public string Name { get { return name; } }
    public float Latitude { get { return latitude; } }
    public float Longitude { get { return longitude; } }
    public float Altitude { get { return altitude; } }
    public Vector2 LatLonVector { get { return new Vector2(Latitude, Longitude); } }

    public Location(string name, float latitude, float longitude, float altitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
        this.altitude = altitude;
    }

    public Location(float latitude, float longitude, float altitude)
                    : this("Unspecified", latitude, longitude, altitude) {}

}
