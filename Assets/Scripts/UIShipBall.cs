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

    private float rotationCurrent = 0;

    void LateUpdate() {
        float posX = offsetX;
        float posY = offsetY;
        float percX = posX / mainCamera.pixelWidth;
        float percY = posY / mainCamera.pixelHeight;
        percX = rightAnchorX ? 1 - percX : percX;
        percY = topAnchorY ? 1 - percY : percY;
        Vector3 pos = mainCamera.ViewportToWorldPoint(new Vector3(percX, percY, mainCamera.nearClipPlane + 1f));
        transform.position = pos;
        transform.rotation = mainCamera.transform.rotation;
        float rotationSpeedNormalized = rotationSpeed * Time.deltaTime;
        rotationCurrent += rotationSpeedNormalized;
        transform.rotation = Quaternion.Euler(0, rotationCurrent, 0);
    }
}
