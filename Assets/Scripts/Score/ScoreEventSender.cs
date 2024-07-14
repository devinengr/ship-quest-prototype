using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEventSender : MonoBehaviour {

    public string searchLabel;
    private ScoreCounter scoreCounter;

    void Start() {
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    public void IncrementScore() {
        scoreCounter.IncrementScore();
    }

}
