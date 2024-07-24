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
    public bool useAlphaTargetAsFarAlpha = false;
    public bool useAlphaTargetAsCloseAlpha = false;

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

    float GetCloseAlpha() {
        if (useAlphaTargetAsCloseAlpha) {
            return alphaTarget.Alpha;
        }
        return 0f;
    }

    float GetFarAlpha() {
        if (useAlphaTargetAsFarAlpha) {
            return alphaTarget.Alpha;
        }
        return 1f;
    }

    float CalculateAlpha() {
        float distance = Vector3.Distance(transform.position, toCompare.transform.position);
        float distanceFromClose = distance - closeDistance;
        float range = farDistance - closeDistance;
        float distanceFromCloseNormalized = distanceFromClose / range;
        float alpha = Mathf.Lerp(GetCloseAlpha(), GetFarAlpha(), distanceFromCloseNormalized);

        if (visibleWhenCloser) {
            alpha = 1 - alpha;
        }
        return alpha;
    }

    void Update() {
        float alpha = CalculateAlpha();
        SharedColorFunctions.SetTransparency(gameObject, alpha);
        if (setAlphaTarget) {
            alphaTarget.Alpha = alpha;
        }
    }

}
