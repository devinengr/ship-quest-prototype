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
    public UnityEvent noTargetOnStartEvent;

    private bool targetMissingEventInvoked = false;

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

    bool ObjectToLookAtExists() {
        return GetClosestByTag() != null;
    }

    void Start() {
        if (!ObjectToLookAtExists()) {
            noTargetOnStartEvent.Invoke();
            targetMissingEventInvoked = true;
        }
    }

    void LateUpdate() {
        GameObject toPointAt = GetClosestByTag();
        if (toPointAt == null) {
            if (!targetMissingEventInvoked) {
                targetMissingEvent.Invoke();
                targetMissingEventInvoked = true;
            }
        } else {
            targetMissingEventInvoked = false;
            transform.LookAt(toPointAt.transform);
            if (lockYAxis) {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            transform.Rotate(offsetRotation);
        }
    }

}
