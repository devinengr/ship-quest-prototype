using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIBGColorChangeOnUprightAngle : MonoBehaviour {
    
    public DeviceGyroscope gyroscope;
    public Color color;

    public int minAngle = 15;
    public int maxAngle = 65;

    private Color colorOld;

    void Start() {
        colorOld = SharedColorFunctions.GetColor(gameObject);
    }

    void Update() {
        float angle = gyroscope.AngleFromPortraitUpright();
        if (minAngle <= angle && angle <= maxAngle) {
            SharedColorFunctions.SetColor(gameObject, color);
        } else {
            SharedColorFunctions.SetColor(gameObject, colorOld);
        }
    }

}
