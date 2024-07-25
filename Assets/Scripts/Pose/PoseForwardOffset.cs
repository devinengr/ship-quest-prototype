using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PoseForwardOffset : MonoBehaviour {

    public GameObject anchor;
    public float forwardOffset;
    public float rightOffset;
    public float upOffset;
    public bool setPositionTarget = false;
    public PositionTarget positionTarget;

    Vector3 GetScaledOffset() {
        Vector3 offset = Vector3.zero;
        offset += transform.forward * forwardOffset;
        offset += transform.right * rightOffset;
        offset += transform.up * upOffset;
        offset = offset.Multiply(anchor.transform.localScale);
        return offset;
    }

    void Update() {
        Vector3 offsetScaled = GetScaledOffset();
        transform.position = anchor.transform.position + offsetScaled;
        if (setPositionTarget) {
            positionTarget.Position = transform.position;
        }
    }

}
