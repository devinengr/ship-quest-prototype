using System;
using System.Collections;
using UnityEngine;

public class FadeOnEvent : MonoBehaviour {

    public float transitionSpeed = 2.5f;

    public float startAlpha = 0f;
    public float endAlpha = 1f;
    public bool useAlphaTarget;
    public AlphaTarget alphaTarget;

    IEnumerator BeginFadeCoroutine() {
        float t = 0f;
        float end = useAlphaTarget ? alphaTarget.Alpha : endAlpha;
        while (t < 1f) {
            t += transitionSpeed * Time.deltaTime;
            float transparency = Mathf.Lerp(startAlpha, end, t);
            SharedColorFunctions.SetTransparency(gameObject, transparency);
            yield return null;
        }
    }

    public void BeginFade() {
        StartCoroutine(BeginFadeCoroutine());
    }

}
