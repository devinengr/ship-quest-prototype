using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorByDistance : MonoBehaviour {

    public GameObject toCompare;
    public float distance = 50f;
    public Color closeColor = Color.cyan;
    public Color farColor = Color.white;

    private Material material;

    void Start() {
        material = GetComponent<Renderer>().material;
    }

    void Update() {
        if (Vector3.Distance(transform.position, toCompare.transform.position) < distance) {
            material.color = closeColor;
        } else {
            material.color = farColor;
        }
    }

}
