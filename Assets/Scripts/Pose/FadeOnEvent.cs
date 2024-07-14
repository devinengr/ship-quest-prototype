using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnEvent : MonoBehaviour {

    public float transitionSpeed = 2.5f;
    public bool correctRenderOrder = true;

    IEnumerator BeginFadeCoroutine() {
        float t = 0f;
        while (t < 1f) {
            t += transitionSpeed * Time.deltaTime;
            float transparency = Mathf.Lerp(1f, 0f, t);
            SharedColorFunctions.SetTransparency(gameObject, transparency);
            yield return null;
        }
    }

    public void BeginFade() {
        StartCoroutine(BeginFadeCoroutine());
    }

    public void MakeInvisibleImmediately() {
        SharedColorFunctions.SetTransparency(gameObject, 0f);
    }

    void Start() {
        if (correctRenderOrder) {
            GetComponent<Renderer>().material.SetInt("_ZWrite", 1);
        }
    }

}
