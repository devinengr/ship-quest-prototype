using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObjectClickHandler : MonoBehaviour {

    public XRScreenSpaceController controller;
    public XRRayInteractor raycastInteractor;
    public UnityEvent actionsOnClick;

    void Start() {
        InputActionProperty tapAction = controller.tapStartPositionAction;
        InputActionProperty dragAction = controller.dragCurrentPositionAction;
        if (Application.isEditor) {
            dragAction.action.canceled += ctx => RaycastCallback(ctx);
        } else {
            tapAction.action.started += ctx => RaycastCallback(ctx);
        }
    }

    void RaycastCallback(InputAction.CallbackContext ctx) {
        if (raycastInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit3D)) {
            GameObject obj = hit3D.transform.gameObject;
            if (obj == gameObject) {
                actionsOnClick.Invoke();
            }
        }
    }

}
