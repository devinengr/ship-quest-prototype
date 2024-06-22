using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShippoParent : MonoBehaviour {

    public GameObject compassCalibrator;
    public Camera mainCamera;
    public CompassData compassData;

    [Tooltip("Number of milliseconds before readjusting object positions.")]
    public float recalibrationInterval;
    private Quaternion recalibrationRotationInitial;
    private Quaternion recalibrationRotationTarget;
    private bool recalibrating = false;

    private long startTime;
    private long currentTime;
    private long elapsedTime;

    void Start() {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    private void DetachHippos() {
        List<GameObject> children = new List<GameObject>();
        transform.gameObject.GetChildGameObjects(children);
        foreach (GameObject child in children) {
            if (child.CompareTag("ShippoTheHippo")) {
                child.transform.SetParent(null);
            }
        }
    }

    private void ReattachHippos() {
        GameObject[] hippos = GameObject.FindGameObjectsWithTag("ShippoTheHippo");
        foreach (GameObject hippo in hippos) {
            hippo.transform.SetParent(transform);
        }
    }

    private float NormalizeElapsedTime() {
        Debug.Log(elapsedTime);
        Debug.Log(elapsedTime / recalibrationInterval);
        return elapsedTime / recalibrationInterval;
    }

    void LateUpdate() {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
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
                recalibrating = true;
                recalibrationRotationInitial = transform.rotation;
                recalibrationRotationTarget = compassCalibrator.transform.rotation;
            }
        }

        if (recalibrating) {
            transform.rotation = Quaternion.Slerp(
                recalibrationRotationInitial,
                recalibrationRotationTarget,
                NormalizeElapsedTime());
        }
    }

}
