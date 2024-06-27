using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipBall : MonoBehaviour {

    public Camera mainCamera;
    public float offsetX = 100;
    public float offsetY = 100;
    public bool rightAnchorX = true;
    public bool topAnchorY = false;
    public float rotationSpeed = 100;

    private void SetPosition(float percX, float percY) {
        Vector3 posScreen = new Vector3(percX, percY, mainCamera.nearClipPlane + 1f);
        Vector3 posWorld = mainCamera.ViewportToWorldPoint(posScreen);
        transform.position = posWorld;
    }

    void Start() {
        float posX = offsetX;
        float posY = offsetY;
        float percX = posX / mainCamera.pixelWidth;
        float percY = posY / mainCamera.pixelHeight;
        percX = rightAnchorX ? 1 - percX : percX;
        percY = topAnchorY ? 1 - percY : percY;
        SetPosition(percX, percY);
    }

    void LateUpdate() {
        float rotation = rotationSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0f, rotation, 0f);
    }
}
