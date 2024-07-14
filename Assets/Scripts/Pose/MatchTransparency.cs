using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchTransparency : MonoBehaviour {

    public GameObject toMatch;

    void LateUpdate() {
        float transparencyToMatch = SharedColorFunctions.GetTransparency(toMatch);
        SharedColorFunctions.SetTransparency(gameObject, transparencyToMatch);
    }

}
