using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowOnEvent : MonoBehaviour {

    public GameObject toShow;

    public void ShowOnEvent() {
        toShow.SetActive(true);
    }

}
