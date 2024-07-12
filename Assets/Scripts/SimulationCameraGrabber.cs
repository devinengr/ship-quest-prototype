using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationCameraGrabber : MonoBehaviour {

    private GameObject simulationCamera;

    void Start() {
        if (simulationCamera == null && Application.isEditor) {
            GameObject[] gameObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject gameObject in gameObjects) {
                if (gameObject.activeInHierarchy) {
                    if (gameObject.name.Equals("SimulationCamera")) {
                        simulationCamera = gameObject;
                        simulationCamera.transform.SetParent(this.transform);
                        break;
                    }
                }
            }
        }
    }

    void Update() {
        if (simulationCamera != null) {
            simulationCamera.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        }
    }

}
