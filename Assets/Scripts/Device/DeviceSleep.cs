using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSleep : MonoBehaviour {

    void Start() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
