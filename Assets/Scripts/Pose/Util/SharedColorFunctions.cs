using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SharedColorFunctions {

    public static bool GameObjectIsTextLabel(GameObject obj) {
        return obj.GetComponent<TMP_Text>() != null;
    }

    public static bool GameObjectIsMesh(GameObject obj) {
        return obj.GetComponent<Renderer>() != null;
    }

    public static bool GameObjectIsImage(GameObject obj) {
        return obj.GetComponent<Image>() != null;
    }

    public static Color GetColor(GameObject obj) {
        if (GameObjectIsTextLabel(obj)) {
            return obj.GetComponent<TMP_Text>().color;
        } else if (GameObjectIsMesh(obj)) {
            return obj.GetComponent<Renderer>().material.color;
        } else if (GameObjectIsImage(obj)) {
            return obj.GetComponent<Image>().color;
        } else {
            throw new NotImplementedException("Handle a new type of component here");
        }
    }

    public static void SetColor(GameObject obj, Color color) {
        if (GameObjectIsTextLabel(obj)) {
            obj.GetComponent<TMP_Text>().color = color;
        } else if (GameObjectIsMesh(obj)) {
            obj.GetComponent<Renderer>().material.color = color;
        } else if (GameObjectIsImage(obj)) {
            obj.GetComponent<Image>().color = color;
        } else {
            throw new NotImplementedException("Handle a new type of component here");
        }
    }

    public static float GetTransparency(GameObject obj) {
        return GetColor(obj).a;
    }

    public static void SetTransparency(GameObject obj, float transparency) {
        Color color = GetColor(obj);
        color.a = transparency;
        SetColor(obj, color);
    }

}
