using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighscore : MonoBehaviour {

    public string prefsHighscoreVarName;

    // Start is called before the first frame update
    void Start() {

        gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(prefsHighscoreVarName, 0).ToString();

    }

    // Update is called once per frame
    void Update() {
        
    }
}
