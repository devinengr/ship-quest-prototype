using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHideIfGyroEnabled : MonoBehaviour {

    public DeviceGyroscope deviceGyroscope;
    public GameObject toHide;

    void Start() {
        if (deviceGyroscope.DeviceGyroscopeIsEnabled) {
            toHide.SetActive(false);
        }
    }

}
