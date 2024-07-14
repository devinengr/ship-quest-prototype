using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SharedColorFunctions : MonoBehaviour {

    public bool GameObjectIsTextLabel(GameObject obj) {
        return obj.GetComponent<TMP_Text>() != null;
    }

    public Color GetColor(GameObject obj) {
        if (GameObjectIsTextLabel(obj)) {
            return obj.GetComponent<TMP_Text>().color;
        } else {
            return obj.GetComponent<Renderer>().material.color;
        }
    }

    public void SetColor(GameObject obj, Color color) {
        if (GameObjectIsTextLabel(obj)) {
            obj.GetComponent<TMP_Text>().color = color;
        } else {
            obj.GetComponent<Renderer>().material.color = color;
        }
    }

    public float GetTransparency(GameObject obj) {
        return GetColor(obj).a;
    }

    public void SetTransparency(GameObject obj, float transparency) {
        Color color = GetColor(obj);
        color.a = transparency;
        SetColor(obj, color);
    }

}
