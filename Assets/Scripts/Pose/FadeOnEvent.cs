using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnEvent : MonoBehaviour {

    public float transitionSpeed = 2.5f;
    public bool fadeOut = true;

    IEnumerator BeginFadeCoroutine() {
        float start = fadeOut ? 1f : 0f;
        float end = fadeOut ? 0f : 1f;
        float t = 0f;
        while (t < 1f) {
            t += transitionSpeed * Time.deltaTime;
            float transparency = Mathf.Lerp(start, end, t);
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

    public void MakeVisibleImmediately() {
        SharedColorFunctions.SetTransparency(gameObject, 1f);
    }

}
