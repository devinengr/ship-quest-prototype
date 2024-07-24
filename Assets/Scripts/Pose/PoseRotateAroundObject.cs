using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseRotateAroundObject : MonoBehaviour {

    public float speed = 50f;
    public Vector3 aboutAxis;
    public GameObject aboutObject;

    void Update() {
        transform.RotateAround(aboutObject.transform.position, aboutAxis, speed * Time.deltaTime);
    }

}
