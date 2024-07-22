using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchTransparency : MonoBehaviour {

    public List<GameObject> toMatch;
    public bool matchMostVisible = true;

    float GetHighestTransparency() {
        float max = 0f;
        foreach (GameObject obj in toMatch) {
            float current = SharedColorFunctions.GetTransparency(obj);
            if (current > max) {
                max = current;
            }
        }
        return max;
    }

    void LateUpdate() {
        float transparencyToMatch = GetHighestTransparency();
        SharedColorFunctions.SetTransparency(gameObject, transparencyToMatch);
    }

}
