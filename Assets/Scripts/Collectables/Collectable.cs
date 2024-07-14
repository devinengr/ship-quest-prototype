using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour {

    private Collector collector;

    public float approachSpeed = 0.25f;
    public float distanceToDestroyAt = 0.02f;
    public UnityEvent onCollectableSpawn;
    public UnityEvent onCollectorApproach;
    public UnityEvent onCollectorReached;

    public bool IsApproachingCollector { get; private set; }

    void Start() {
        onCollectableSpawn.Invoke();
        collector = FindObjectOfType<Collector>();
    }

    public IEnumerator ApproachCollector() {
        onCollectorApproach.Invoke();
        IsApproachingCollector = true;
        float t = 0f;
        while (Vector3.Distance(transform.position, collector.transform.position) > distanceToDestroyAt) {
            transform.position = Vector3.Lerp(transform.position, collector.transform.position, t);
            transform.localScale = Vector3.Lerp(transform.localScale, collector.transform.localScale, t);
            t += approachSpeed * Time.deltaTime;
            yield return null;
        }
        onCollectorReached.Invoke();
        Destroy(transform.gameObject);
    }

}
