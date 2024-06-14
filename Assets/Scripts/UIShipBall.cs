using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipBall : MonoBehaviour {

    public Camera mainCamera;
    public float offsetX;
    public float offsetY;
    public bool rightAnchorX;
    public bool topAnchorY;
    public float rotationSpeed;

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
