using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSleep : MonoBehaviour {

    void Start() {
        // prevent the device from going to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
