using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TransparentByDistance : MonoBehaviour {

    public bool searchForObject = false;
    public string searchByTag = "";
    public GameObject toCompare;
    public float farDistance = 6f;
    public float closeDistance = 3f;
    public bool visibleWhenCloser = false;
    public bool setAlphaTarget = false;
    public AlphaTarget alphaTarget;

    void Start() {
        if (searchForObject) {
            toCompare = GameObject.FindGameObjectWithTag(searchByTag);
        }
    }

    public void EnableBehavior() {
        enabled = true;
    }

    public void DisableBehavior() {
        enabled = false;
    }

    float CalculateAlpha() {
        float distance = Vector3.Distance(transform.position, toCompare.transform.position);
        float distanceClamped = Mathf.Clamp(distance, closeDistance, farDistance);
        float range = Mathf.Abs(farDistance - closeDistance);
        float distanceRaw = distanceClamped - closeDistance;
        float alpha = distanceRaw / range;
        if (visibleWhenCloser) {
            alpha = 1 - alpha;
        }
        return alpha;
    }

    void Update() {
        Color color = SharedColorFunctions.GetColor(gameObject);
        color.a = CalculateAlpha();
        SharedColorFunctions.SetColor(gameObject, color);
        if (setAlphaTarget) {
            alphaTarget.Alpha = color.a;
        }
    }

}
