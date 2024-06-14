using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public int score { get; set; } = 0;

    public TMP_Text label;

    void Update() {
        label.text = "Score: " + score;
    }

}
