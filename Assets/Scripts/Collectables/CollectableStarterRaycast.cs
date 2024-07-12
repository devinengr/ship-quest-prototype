using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class CollectableStarterRaycast : MonoBehaviour {

    public XRScreenSpaceController controller;
    public XRRayInteractor raycastInteractor;
    public Camera mainCamera;
    public float maxDistance = 50f;

    void Start() {
        InputActionProperty tapAction = controller.tapStartPositionAction;
        InputActionProperty dragAction = controller.dragCurrentPositionAction;
        if (Application.isEditor) {
            dragAction.action.canceled += ctx => RaycastCallback(ctx);
        } else {
            tapAction.action.started += ctx => RaycastCallback(ctx);
        }
    }

    Collectable GetScript(GameObject obj) {
        return obj.GetComponent<Collectable>();
    }

    bool IsCollectable(GameObject obj) {
        return GetScript(obj) != null;
    }

    bool IsCloseToCamera(GameObject obj) {
        return Vector3.Distance(mainCamera.transform.position, obj.transform.position) < maxDistance;
    }
    
    void RaycastCallback(InputAction.CallbackContext ctx) {
        if (raycastInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit3D)) {
            GameObject obj = hit3D.transform.gameObject;
            if (IsCollectable(obj) && IsCloseToCamera(obj)) {
                Collectable collectable = GetScript(obj);
                if (!collectable.IsApproachingCollector) {
                    StartCoroutine(collectable.ApproachCollector());
                }
            }
        }
    }

}
