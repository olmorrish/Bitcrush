using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FireMode { tromino, tetromino, pentomino, all };

public class PersistentSettings : MonoBehaviour {

    public string fireMode;

    //TODO APPLY THESE ON LOAD
    public bool rotate45;

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
     * 
     */
    void ApplyGameModeSettings(Scene scene, LoadSceneMode mode) {
        Debug.Log("Loaded Scene: \"" + scene.name + "\" in mode: \"" + mode + "\"");

        if (scene.name.Equals("Game")) {
            Debug.Log("Beginning application of game scene settings.");

            GamePattern gamePattern = GameObject.Find("GameMaster").GetComponent<GamePattern>();
            gamePattern.fireMode = fireMode;
            Debug.Log(">> Firemode set to \"" + fireMode + "\"");

            gamePattern.rotateBy45Degrees = rotate45;
            Debug.Log(">> 45 degree skew set to: " + rotate45);

        }
    }
}
