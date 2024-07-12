using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHideIfLocationEnabled : MonoBehaviour {

    public DeviceLocation deviceLocation;
    public GameObject toHide;

    void Start() {
        if (deviceLocation.DeviceLocationIsEnabled) {
            toHide.SetActive(false);
        }
    }

}
