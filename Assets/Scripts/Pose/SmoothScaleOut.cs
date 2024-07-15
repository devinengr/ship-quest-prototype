using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothScaleOut : MonoBehaviour {

    public float transitionSpeed = 2.5f;

    IEnumerator BeginScaling() {
        float t = 0f;
        Vector3 scale = transform.localScale;
        while (t < 1f) {
            t += transitionSpeed * Time.deltaTime;
            Vector3 scaleNew = Vector3.Lerp(scale, new(0, 0, 0), t);
            transform.localScale = scaleNew;
            yield return null;
        }
    }

    public void BeginScaleOut() {
        StartCoroutine(BeginScaling());
    }

}
