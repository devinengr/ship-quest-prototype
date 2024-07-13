using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableLabel : MonoBehaviour {

    public Collectable collectable;
    public TMP_Text label;

    void Start() {
        label.text = collectable.name;
    }

}
