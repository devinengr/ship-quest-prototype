using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseLookAt : MonoBehaviour {

    public bool searchForObject = false;
    public string searchByTag = "";
    public GameObject lookAt;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;

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
        transform.Rotate(new Vector3(xOffset, yOffset, zOffset));
    }

}
