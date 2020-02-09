using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FireMode { tromino, tetromino, pentomino, all };

public class PersistentSettings : MonoBehaviour {

    public string fireMode;
    public bool rotate45;
    public float minWaitTime;
    public float maxWaitTime;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject); //ensures the objects always persists
    }

    void OnEnable() {
        //Tell our 'ApplyGameModeSettings' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += ApplyGameModeSettings;
    }

    void OnDisable() {
        //Tell our 'ApplyGameModeSettings' function to stop listening for a scene change as soon as this script is disabled. 
        SceneManager.sceneLoaded -= ApplyGameModeSettings;
    }

    /* This function is called once upon game scene loading.
     *  It's not efficient and is pretty bulky, but it's only called once per game load
     *  Note: "GameObject.Find" calls are necessary since scene references can't be set before scene exists
     */
    void ApplyGameModeSettings(Scene scene, LoadSceneMode mode) {
        Debug.Log("Loaded Scene: \"" + scene.name + "\" in mode: \"" + mode + "\"");

        if (scene.name.Equals("Game")) {
            Debug.Log("Beginning application of game scene settings.");
            GamePattern gamePattern = GameObject.Find("GameMaster").GetComponent<GamePattern>();

            gamePattern.fireMode = fireMode;                // firemode
            gamePattern.rotateBy45Degrees = rotate45;       // roation 
            gamePattern.minWaitTime = minWaitTime;          // min time between block throws
            gamePattern.maxWaitTime = maxWaitTime;          // max time between block throws
        }
    }
}
