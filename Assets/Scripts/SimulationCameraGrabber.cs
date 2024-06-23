using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationCameraGrabber : MonoBehaviour {

    // the simulation camera is generated when running in the editor, so
    // keep this private
    private GameObject simulationCamera;

    void Start() {
        if (simulationCamera == null && Application.isEditor) {
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
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
            simulationCamera.transform.localPosition = new Vector3(0, 0, 0);
            simulationCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
