using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SharedColorFunctions))]
public class MatchTransparency : MonoBehaviour {

    public GameObject toMatch;

    public SharedColorFunctions ColorUtil { get { return GetComponent<SharedColorFunctions>(); } }

    void LateUpdate() {
        float transparencyToMatch = ColorUtil.GetTransparency(toMatch);
        ColorUtil.SetTransparency(gameObject, transparencyToMatch);
    }

}
