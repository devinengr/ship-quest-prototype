using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public List<Color> colors;
    public float transitionSpeed = 1f;
    
    private int index = 0;
    private bool readyForNextColor = true;

    private Color currentColor;

    Color GetCurrentColor() {
        return colors[index];
    }

    Color GetNextColor() {
        if (index + 1 == colors.Count) {
            return colors[0];
        }
        return colors[index + 1];
    }

    void UpdateIndex() {
        index++;
        if (index >= colors.Count) {
            index = 0;
        }
    }

    IEnumerator UpdateColorSmoothly() {
        float t = 0;
        while (t <= 1f) {
            Color target = Color.Lerp(GetCurrentColor(), GetNextColor(), t);
            // coroutines occur after Update but before LateUpdate. prefer
            // this one to effectively occur in Update, so store the color
            // as an instance variable and update it during the next frame in
            // Update.
            currentColor = target;
            t += transitionSpeed * Time.deltaTime;
            yield return null;
        }
        UpdateIndex();
        readyForNextColor = true;
    }

    void Update() { 
        if (readyForNextColor) {
            readyForNextColor = false;
            StartCoroutine(UpdateColorSmoothly());
        }
        if (currentColor != null) {
            SharedColorFunctions.SetColor(gameObject, currentColor);
        }
    }

}
