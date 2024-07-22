using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHideIfLocationDisabled : MonoBehaviour {

    public DeviceLocation deviceLocation;
    public GameObject toHide;

    void Start() {
        if (!deviceLocation.DeviceLocationIsEnabled) {
            toHide.SetActive(false);
        }
    }

}
