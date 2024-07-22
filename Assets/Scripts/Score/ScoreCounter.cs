using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public TMP_Text label;

    public int Score { get; private set; } = 0;

    public void IncrementScore() {
        Score++;
    }

    void Update() {
        label.text = "Score: " + Score;
    }

}
