using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HippoCollector : MonoBehaviour {

    public XRScreenSpaceController controller;
    public XRRayInteractor raycastInteractor;

    public ScoreCounter scoreCounter;
    public Camera camera;

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
        RaycastHit hit3D;
        if (raycastInteractor.TryGetCurrent3DRaycastHit(out hit3D)) {
            if (hit3D.transform.gameObject.CompareTag("ShippoTheHippo")) {
                ShippoCollectable collectable = hit3D.transform.gameObject.GetComponent<ShippoCollectable>();
                Vector3 camPos = camera.transform.position;
                Vector3 objPos = hit3D.transform.position;
                if (Vector3.Distance(camPos, objPos) < collectable.interactionDistance) {
                    collectable.grabbed = true;
                    scoreCounter.score++;
                }
            }
        }
    }

}
