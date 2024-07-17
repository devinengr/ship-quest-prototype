using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FadeOnEvent))]
public class MakeChildrenVisibleOnCalibrateEvent : MonoBehaviour {

    private bool calibrated = false;
    private FadeOnEvent fadeOnEvent;

    List<GameObject> GetChildren() {
        List<GameObject> children = new();
        gameObject.GetChildGameObjects(children);
        return children;
    }

    public void OnCalibrateEvent() {
        calibrated = true;
        foreach (GameObject child in GetChildren()) {
            fadeOnEvent.BeginFade(child);
        }
    }

    void MakeChildrenInvisibleIfNotCalibrated() {
        if (!calibrated) {
            foreach (GameObject child in GetChildren()) {
                fadeOnEvent.MakeInvisibleImmediately(child);
            }
        }
    }

    void Start() {
        fadeOnEvent = GetComponent<FadeOnEvent>();
        MakeChildrenInvisibleIfNotCalibrated();
    }

    void LateUpdate() {
        MakeChildrenInvisibleIfNotCalibrated();
    }

}
