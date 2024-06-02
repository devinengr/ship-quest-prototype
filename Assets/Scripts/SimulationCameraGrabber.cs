using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationCameraGrabber : MonoBehaviour
{
    // todo set this as Camera, not GameObject
    [SerializeField]
    [Tooltip("The simulation camera is loaded by XR Simulation when the scene starts. The script will automatically fetch it.")]
    private GameObject simulationCamera;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (simulationCamera != null) {
            simulationCamera.transform.localPosition = new Vector3(0, 0, 0);
            simulationCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
