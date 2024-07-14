using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseLookAt : MonoBehaviour {

    public bool searchForObject = false;
    public string searchByTag = "";
    public GameObject lookAt;

    public void UpdateLookAtObject(GameObject obj) {
        lookAt = obj;
    }

    void Start() {
        if (searchForObject) {
            lookAt = GameObject.FindGameObjectWithTag(searchByTag);
        }
    }

    void Update() {
        transform.LookAt(lookAt.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }

}
