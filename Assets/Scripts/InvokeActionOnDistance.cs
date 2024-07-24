using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeActionOnDistance : MonoBehaviour {

    public bool searchForObject = false;
    public string searchByTag;
    public GameObject toCompare;
    public float distanceThreshold;
    public UnityEvent actionOnCloser;
    public UnityEvent actionOnFurther;

    private bool closerInvoked = false;
    private bool furtherInvoked = false;

    void InvokeCloser() {
        if (!closerInvoked) {
            furtherInvoked = false;
            closerInvoked = true;
            actionOnCloser.Invoke();
        }
    }

    void InvokeFurther() {
        if (!furtherInvoked) {
            furtherInvoked = true;
            closerInvoked = false;
            actionOnFurther.Invoke();
        }
    }

    void Start() {
        if (searchForObject) {
            toCompare = GameObject.FindGameObjectWithTag(searchByTag);
        }
    }

    void Update() {
        float distance = Vector3.Distance(transform.position, toCompare.transform.position);
        if (distance < distanceThreshold) {
            InvokeCloser();
        } else {
            InvokeFurther();
        }
    }

}
