using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectFadeRenderOrder : MonoBehaviour {

    void Start() {
        GetComponent<Renderer>().material.SetInt("_ZWrite", 1);
    }

}
