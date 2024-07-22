using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothScaleIn : MonoBehaviour {

    public float transitionSpeed = 2.5f;
    public bool beginScaledOut = true;

    private Vector3 scaleOriginal;

    IEnumerator BeginScaling() {
        float t = 0f;
        while (t < 1f) {
            t += transitionSpeed * Time.deltaTime;
            Vector3 scaleNew = Vector3.Lerp(new(0, 0, 0), scaleOriginal, t);
            transform.localScale = scaleNew;
            yield return null;
        }
    }

    public void BeginScaleIn() {
        StartCoroutine(BeginScaling());
    }

    void Start() {
        scaleOriginal = transform.localScale;
        if (beginScaledOut) {
            transform.localScale = new(0, 0, 0);
        }
    }

}
