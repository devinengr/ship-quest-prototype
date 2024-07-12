using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public Collector collector;

    public float approachSpeed = 2.5f;

    public bool IsApproachingCollector { get; private set; }

    public IEnumerator ApproachCollector() {
        IsApproachingCollector = true;
        float t = 0f;
        Vector3 position = transform.position;
        Vector3 localScale = transform.localScale;
        while (Vector3.Distance(transform.position, collector.transform.position) > 0.02f) {
            transform.position = Vector3.Lerp(position, collector.transform.position, t);
            transform.localScale = Vector3.Lerp(localScale, collector.transform.localScale, t);
            t += approachSpeed * Time.deltaTime;
            yield return null;
        }
        Destroy(transform.gameObject);
    }

}
