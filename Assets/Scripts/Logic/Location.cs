using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public struct Location {

    public string name;
    public float latitude;
    public float longitude;
    public float altitude;

    public Location(string name, float latitude, float longitude, float altitude) {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
        this.altitude = altitude;
    }

    public Location(float latitude, float longitude, float altitude)
                    : this("Unspecified", latitude, longitude, altitude) {}

}
