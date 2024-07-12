using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseRotate : MonoBehaviour {

    public float speed = 50f;

    void Update() {
        transform.rotation *= Quaternion.Euler(0, speed * Time.deltaTime, 0);
    }

}
