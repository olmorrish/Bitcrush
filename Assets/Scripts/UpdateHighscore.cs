using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighscore : MonoBehaviour {

    public bool addNegativeSign = false;
    public string prefsHighscoreVarName;

    // Start is called before the first frame update
    void Start() {

        Text scoreText = gameObject.GetComponent<Text>();
        int currentHighscore = PlayerPrefs.GetInt(prefsHighscoreVarName, 0);
        scoreText.text = currentHighscore.ToString(); //all highscores default to 0

        if (addNegativeSign && currentHighscore > 0) {
            scoreText.text = "-" + scoreText.text;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
