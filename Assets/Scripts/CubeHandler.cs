using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Internal;

public class CubeHandler : MonoBehaviour {

    public XRRayInteractor raycastInteractor;
    public GameObject spawnObject;
    public XRScreenSpaceController controller;

    void Start() {
        InputActionProperty tapAction = controller.tapStartPositionAction;
        InputActionProperty dragAction = controller.dragCurrentPositionAction;
        if (Application.isEditor) {
            dragAction.action.canceled += ctx => OnDragEnd(ctx);
        } else {
            tapAction.action.started += ctx => OnTapStart(ctx);
        }
    }
    
    void OnTapStart(InputAction.CallbackContext ctx) {
        RaycastHit hit3D;
        if (raycastInteractor.TryGetCurrent3DRaycastHit(out hit3D)) {
            if (hit3D.transform.gameObject.GetComponent<ARPlane>() != null) {
                GameObject spawned = Instantiate(spawnObject);
                spawned.transform.position = hit3D.point += new Vector3(0, 0.1f, 0);
            }
        }
    }

    void OnDragEnd(InputAction.CallbackContext ctx) {
        OnTapStart(ctx);
    }

}
