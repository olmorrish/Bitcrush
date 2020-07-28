﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FireMode { tromino, tetromino, pentomino, all };

public class PersistentSettings : MonoBehaviour {

    [HideInInspector] public string fireMode;
    [HideInInspector] public bool rotate45;
    [HideInInspector] public bool slipperyJumpAllowed;
    [HideInInspector] public float boostCooldown;
    [HideInInspector] public bool immuneToCrush;
    [HideInInspector] public float minWaitTime;
    [HideInInspector] public float maxWaitTime;
    [HideInInspector] public bool flipCamera;
    [HideInInspector] public bool makeScoreNegative;
    [HideInInspector] public BlockPalette settingPalette;
    [HideInInspector] public BlockPalette optionOverridePalette;
    [HideInInspector] public bool impossibleMode;

    //Singleton
    static PersistentSettings instance;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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

        if (scene.name.Equals("Game")) {
            GameObject gameMaster = GameObject.Find("GameMaster");

            GamePattern gamePattern = gameMaster.GetComponent<GamePattern>();

            gamePattern.fireMode = fireMode;                    // firemode
            gamePattern.rotateBy45Degrees = rotate45;           // roation 
            gamePattern.minWaitTime = minWaitTime;              // min time between block throws
            gamePattern.maxWaitTime = maxWaitTime;              // max time between block throws

            //apply a palette - based on player options or gamemode
            if (optionOverridePalette != null && !impossibleMode)
                gamePattern.currentPalette = optionOverridePalette; //use player selection instead if there is one (only exception is impossible mode!)
            else
                gamePattern.currentPalette = settingPalette;        // the colour palette object to pull from - passes through Pattern into throw() 

            ScoreData scoreData = gameMaster.GetComponent<ScoreData>();
            scoreData.makeScoreNegative = makeScoreNegative;

            GameObject cameraMain = GameObject.Find("Main Camera");
            GameObject cameraKillLine = GameObject.Find("KillLine Camera");
            if (flipCamera)
                cameraMain.transform.eulerAngles = new Vector3(0, 0, 180);
            else
                cameraMain.transform.eulerAngles = new Vector3(0, 0, 0);    //need this line to reset when changing between gamemodes in same session
            if (flipCamera)
                cameraKillLine.transform.eulerAngles = new Vector3(0, 0, 180);
            else
                cameraKillLine.transform.eulerAngles = new Vector3(0, 0, 0);    //need this line to reset when changing between gamemodes in same session

            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.slipperyJumpAllowed = slipperyJumpAllowed;
            Boost boost = GameObject.Find("Player").GetComponent<Boost>();
            boost.cooldownLength = boostCooldown;
            Exploder exploder = GameObject.Find("Player").GetComponent<Exploder>();
            exploder.immuneToCrush = immuneToCrush;
        }
    }
}
