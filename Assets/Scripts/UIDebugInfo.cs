using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class UIDebugInfo : MonoBehaviour {

    public DeviceCompass compassData;
    public DeviceLocation locationData;
    public DeviceGyroscope gyroscopeData;
    public ARSession sessionAR;
    public PlaneCalibratableParent planeParent;

    void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;
        GUILayout.Label("Compass: " + compassData.LastAvg);
        GUILayout.Label("Latitude: " + locationData.Current.Latitude);
        GUILayout.Label("Longitude: " + locationData.Current.Longitude);
        GUILayout.Label("Compass Calibration Count: " + planeParent.CalibrationCount);
        GUILayout.Label("Compass Calibration Time: " + (float)planeParent.CalibrationTime/1000 + " s");
        GUILayout.Label("Camera Upright Angle (Portrait): " + gyroscopeData.AngleFromPortraitUpright());
        GUILayout.Label("Camera Upright Angle (Landscape): " + gyroscopeData.AngleFromLandscapeUpright());
    }

}
