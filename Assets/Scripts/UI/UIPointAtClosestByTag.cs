using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIPointAtClosestByTag : MonoBehaviour {

    public GameObject toCompare;
    public string searchTag;
    public Vector3 offsetRotation;
    public bool lockYAxis = false;
    public UnityEvent targetMissingEvent;
    public UnityEvent targetFoundEvent;

    private bool targetMissingEventInvoked = true;
    private bool targetFoundEventInvoked = false;

    void InvokeTargetMissing() {
        if (!targetMissingEventInvoked) {
            targetFoundEventInvoked = false;
            targetMissingEventInvoked = true;
            targetMissingEvent.Invoke();
        }
    }

    void InvokeTargetFound() {
        if (!targetFoundEventInvoked) {
            targetMissingEventInvoked = false;
            targetFoundEventInvoked = true;
            targetFoundEvent.Invoke();
        }
    }

    GameObject GetClosestByTag() {
        float distanceClosest = float.MaxValue;
        GameObject closest = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(searchTag)) {
            float distance = Vector3.Distance(toCompare.transform.position, obj.transform.position);
            if (distance < distanceClosest) {
                closest = obj;
                distanceClosest = distance;
            }
        }
        return closest;
    }

    void PerformLookAt(GameObject toPointAt) {
        transform.LookAt(toPointAt.transform);
        if (lockYAxis) {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        transform.Rotate(offsetRotation);
    }

    void LateUpdate() {
        GameObject toPointAt = GetClosestByTag();
        if (toPointAt == null) {
            InvokeTargetMissing();
        } else {
            InvokeTargetFound();
            PerformLookAt(toPointAt);
        }
    }

}
