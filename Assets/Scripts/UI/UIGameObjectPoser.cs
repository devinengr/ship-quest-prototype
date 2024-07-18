using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameObjectPoser : MonoBehaviour {

    public RectTransform canvasTransform;
    public float offsetX = 100;
    public float offsetY = 100;
    public bool useOffsetX = true;
    public bool useOffsetY = true;
    public bool rightAnchorX = true;
    public bool topAnchorY = false;
    public bool centeredAlongX = false;
    public bool centeredAlongY = false;

    Vector3 CalculateBottomLeftPosition() {
        float x = -(canvasTransform.rect.width / 2);
        float y = -(canvasTransform.rect.height / 2);
        return new(x, y, 0);
    }

    Vector3 CalculateLocalPosition() {
        float percX = offsetX / canvasTransform.rect.width;
        float percY = offsetY / canvasTransform.rect.height;
        percX = rightAnchorX ? 1 - percX : percX;
        percY = topAnchorY ? 1 - percY : percY;
        float posX = canvasTransform.rect.width * percX;
        float posY = canvasTransform.rect.height * percY;
        return CalculateBottomLeftPosition() + new Vector3(posX, posY, 0);
    }

    void CalculateCenteredOffsets() {
        if (centeredAlongX) {
            offsetX = canvasTransform.rect.width / 2;
        }
        if (centeredAlongY) {
            offsetY = canvasTransform.rect.height / 2;
        }
    }

    void SetNewPosition() {
        Vector3 posNew = CalculateLocalPosition();
        Vector3 posOld = transform.position;
        if (!useOffsetX) {
            posNew.x = posOld.x; 
        }
        if (!useOffsetY) {
            posNew.y = posOld.y;
        }
        transform.localPosition = posNew;
    }

    void Start() {
        CalculateCenteredOffsets();
        SetNewPosition();
    }

    void Update() {
        // Editor window can be resized, phones usually can't
        if (Application.isEditor) {
            Start();
        }
    }

}
