using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
public class PersistentSettings : MonoBehaviour {

    public enum BlockMode {tromino, tetromino, pentomino, all};

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

    void ApplyGameModeSettings(Scene scene, LoadSceneMode mode) {
        Debug.Log("Loaded Scene: \"" + scene.name + "\" in mode: \"" + mode + "\"");


    }
}
