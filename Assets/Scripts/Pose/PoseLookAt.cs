using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseLookAt : MonoBehaviour {

    public GameObject lookAt;

    void Update() {
        transform.LookAt(lookAt.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

}
