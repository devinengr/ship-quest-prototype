using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUICube : MonoBehaviour {

    public Camera mainCamera;
    public GameObject cube;
    public float offsetX;
    public float offsetY;
    public bool rightAnchorX;
    public bool topAnchorY;
    public float rotationSpeed;

    private Quaternion rotCustom;

    void Start() {
        rotCustom = Quaternion.Euler(rotationSpeed, rotationSpeed, rotationSpeed);
    }

    void LateUpdate() {
        float posX = offsetX;
        float posY = offsetY;
        float percX = posX / mainCamera.pixelWidth;
        float percY = posY / mainCamera.pixelHeight;
        percX = rightAnchorX ? 1 - percX : percX;
        percY = topAnchorY ? 1 - percY : percY;
        Vector3 pos = mainCamera.ViewportToWorldPoint(new Vector3(percX, percY, mainCamera.farClipPlane - 1f));
        cube.transform.position = pos;
        cube.transform.rotation = mainCamera.transform.rotation;
        float rotationSpeedNormalized = rotationSpeed * Time.deltaTime;
        rotCustom = rotCustom * Quaternion.Euler(new Vector3(
            rotationSpeedNormalized,
            rotationSpeedNormalized,
            rotationSpeedNormalized
        ));
        Quaternion rotNew = cube.transform.rotation * rotCustom;
        cube.transform.rotation = rotNew;
    }
}
