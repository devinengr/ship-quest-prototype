using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SharedColorFunctions))]
public class TransparentByDistance : MonoBehaviour {

    public bool searchForObject = false;
    public string searchByTag = "";
    public GameObject toCompare;
    public float farDistance = 6f;
    public float closeDistance = 3f;

    public SharedColorFunctions ColorUtil { get { return GetComponent<SharedColorFunctions>(); } }

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
        return alpha;
    }

    void Update() {
        Color color = ColorUtil.GetColor(gameObject);
        color.a = CalculateAlpha();
        ColorUtil.SetColor(gameObject, color);
    }

}
