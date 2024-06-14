using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippoCollectable : MonoBehaviour {

    public bool grabbed { get; set; } = false;

    void Update() {
        transform.rotation *= Quaternion.Euler(0, 50 * Time.deltaTime, 0);
    }

}
