using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour{

    private Text fpsText;

    // Start is called before the first frame update
    void Start() {
        fpsText = GetComponent<Text>();
        fpsText.text = "FPS: 0";
    }

    // Update is called once per frame
    void Update() {
        fpsText.text = "FPS: " + (Mathf.Floor(1f / Time.unscaledDeltaTime)).ToString();
    }
}
