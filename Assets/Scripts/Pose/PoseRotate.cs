using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseRotate : MonoBehaviour {

    public float speed = 50f;
    public Vector3 aboutAxis;

    void Update() {
        Vector3 rotation = aboutAxis * speed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(rotation);
    }

}
