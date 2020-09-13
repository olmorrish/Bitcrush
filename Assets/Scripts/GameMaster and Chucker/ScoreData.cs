using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Score Data
 * Contains score data during a game. Contains methods to update and save scores. 
 */
public class ScoreData : MonoBehaviour{

    public GameMode currentMode; //set by persistentsettings so we know which highscore to update

    private float height;
    [HideInInspector] public float localHighScore;              //the *LOCAL* highscore is just for the current game; starts at zero
    [HideInInspector] public bool makeScoreNegative = false;

    public Text gameplayScore;
    public Text gameOverScore;

    public GameObject player;
    public GameObject scoreLine;

    // Start is called before the first frame update
    void Start() {
        localHighScore = 0;
        PushNewLocalHighScore(0f);
    }

    // Update is called once per frame
    void Update() {
        height = player.transform.position.y;
        if (height > localHighScore) {
            localHighScore = height;
            PushNewLocalHighScore(localHighScore);
        }
    }

    /* Push New Local Highscore
     * Takes a float rounds it up to the nearest int, displaying it as the current score. 
     */
    void PushNewLocalHighScore(float score) {
        if (makeScoreNegative) {
            gameplayScore.text = "SCORE: -" + Mathf.Ceil(score).ToString();
            gameOverScore.text = "FiNAL SCORE: -" + Mathf.Ceil(score).ToString();
        }
        else {
            gameplayScore.text = "SCORE: " + Mathf.Ceil(score).ToString();
            gameOverScore.text = "FiNAL SCORE: " + Mathf.Ceil(score).ToString();
        }
        scoreLine.transform.position = new Vector3(0, score, 0);    //don't need to change this; the game itself isn't different; the camera is just flipped!
    }

    /* Game Over Save Highscore
     * Saves a highscore if needed once the player loses. 
     * Called by the GameOverMenu when a GameOver is detected
     */
    public void GameOverSaveHighScore() {
        Debug.Log("Game Over: Attempting to update highscores.");

        string prefSaveName = currentMode.ToString();

        if (localHighScore > PlayerPrefs.GetInt("HS_" + prefSaveName, 0)) {
            PlayerPrefs.SetInt("HS_" + prefSaveName, (int)Mathf.Ceil(localHighScore));
        }
    }
}
