using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour {

    public string searchTag;
    public float approachSpeed = 0.25f;
    public float distanceToDestroyAt = 0.02f;
    public UnityEvent onCollectableSpawn;
    public UnityEvent onCollectorApproach;
    public UnityEvent onCollectorReached;

    private GameObject collector;

    public bool IsApproachingCollector { get; private set; }

    void Start() {
        onCollectableSpawn.Invoke();
        collector = GameObject.FindGameObjectWithTag(searchTag);
    }

    public IEnumerator ApproachCollector() {
        onCollectorApproach.Invoke();
        IsApproachingCollector = true;
        float t = 0f;
        while (Vector3.Distance(transform.position, collector.transform.position) > distanceToDestroyAt) {
            transform.position = Vector3.Lerp(transform.position, collector.transform.position, t);
            transform.localScale = Vector3.Lerp(transform.localScale, collector.transform.lossyScale, t);
            t += approachSpeed * Time.deltaTime;
            yield return null;
        }
        onCollectorReached.Invoke();
        Destroy(transform.gameObject);
    }

}
