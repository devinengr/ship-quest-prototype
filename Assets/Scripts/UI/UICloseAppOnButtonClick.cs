using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseAppOnButtonClick : MonoBehaviour {

    public Button button;

    void OnButtonClick() {
        Application.Quit();
    }

    void Start() {
        button.onClick.AddListener(OnButtonClick);
    }

}
