using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShippoParent : MonoBehaviour {

    public GameObject compassCalibrator;
    public Camera mainCamera;
    public CompassData compassData;

    [Tooltip("How many seconds before readjusting object positions.")]
    public long recalibrationInterval;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    void Start() {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
    }

    void DetachHippos() {
        List<GameObject> children = new List<GameObject>();
        transform.gameObject.GetChildGameObjects(children);
        foreach (GameObject child in children) {
            if (child.CompareTag("ShippoTheHippo")) {
                child.transform.SetParent(null);
            }
        }
    }

    void ReattachHippos() {
        GameObject[] hippos = GameObject.FindGameObjectsWithTag("ShippoTheHippo");
        foreach (GameObject hippo in hippos) {
            hippo.transform.SetParent(transform);
        }
    }

    void LateUpdate() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
        elapsedTime = currentTime - startTime;
        
        // temporarily detach the hippos while adjusting the position.
        // this will allow the camera to get closer to the them as the
        // player moves toward them.
        DetachHippos();
        transform.position = mainCamera.transform.position;
        ReattachHippos();

        // after a number of seconds, readjust the rotation of the parent
        // of the hippos. this will readjust the position of the hippos
        // according to new compass data.
        if (elapsedTime >= recalibrationInterval) {
            // check if the compass average is stable before readjusting.
            if (compassData.stable) {
                startTime = currentTime;
                transform.rotation = compassCalibrator.transform.rotation;
            }
        }
    }

}
