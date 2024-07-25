using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PoseOscillateForward : MonoBehaviour {

    [Tooltip("This ensures the position is scaled correctly based on the parent's size.")]
    public GameObject parent;
    public PositionTarget positionTarget;
    public float alongForwardAxis = 0f;
    public float alongRightAxis = 0f;
    public float alongUpAxis = 0f;
    public float speed = 2.5f;

    private Vector3 posStart;
    private Vector3 posEnd;
    private float t = 0f;

    // todo duplicate from PoseForwardOffset.cs
    Vector3 GetScaledOffset() {
        Vector3 offset = Vector3.zero;
        offset += transform.forward * alongForwardAxis;
        offset += transform.right * alongRightAxis;
        offset += transform.up * alongUpAxis;
        offset = offset.Multiply(parent.transform.localScale);
        return offset;
    }

    float SinNormalized(float angle) {
        return (Mathf.Sin(angle) + 1) / 2f;
    }

    void Update() {
        Vector3 offsetScaled = GetScaledOffset();
        posStart = positionTarget.Position - offsetScaled;
        posEnd = positionTarget.Position + offsetScaled;
        transform.position = Vector3.Lerp(posStart, posEnd, SinNormalized(t));
        t += speed * Time.deltaTime;
    }
}
