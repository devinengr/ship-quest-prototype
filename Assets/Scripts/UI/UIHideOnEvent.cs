using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHideOnEvent : MonoBehaviour {

    public GameObject toHide;

    public void HideOnEvent() {
        toHide.SetActive(false);
    }

}
