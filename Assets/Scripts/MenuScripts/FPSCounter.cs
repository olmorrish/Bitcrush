using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour{

    private Text fpsText;
    private float nextUpdatePoint;
    private float secondsBetweenRefreshes = 0.2f;
    bool refresh;

    // Start is called before the first frame update
    void Start() {
        fpsText = GetComponent<Text>();
        fpsText.text = "FPS: 0";
        nextUpdatePoint = Time.time;
        refresh = true;
    }

    // Update is called once per frame
    void Update() {
        refresh = (Time.time > nextUpdatePoint);
        
        if (refresh) {
            fpsText.text = "FPS: " + (Mathf.Floor(1f / Time.unscaledDeltaTime)).ToString();
            nextUpdatePoint = Time.time + secondsBetweenRefreshes;
        }
    }
}
