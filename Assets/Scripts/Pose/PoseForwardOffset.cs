using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PoseForwardOffset : MonoBehaviour {

    public GameObject anchor;
    public float forwardOffset;
    public float rightOffset;
    public float upOffset;

    void LateUpdate() {
        Vector3 offset = Vector3.zero;
        offset += transform.forward * forwardOffset;
        offset += transform.right * rightOffset;
        offset += transform.up * upOffset;
        Vector3 offsetScaled = offset.Multiply(anchor.transform.localScale);
        transform.position = anchor.transform.position + offsetScaled;
    }

}
