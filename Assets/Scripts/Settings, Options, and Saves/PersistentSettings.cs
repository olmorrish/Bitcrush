﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum FireMode {
    tromino,
    tetromino,
    pentomino,
    all };

public enum GameMode {
    Normal,
    Casual,
    Tromino,
    Pentomino,
    Tiny,
    Bitcrusher,
    SlipperySlopes,
    UpsideDown,
    UniversitySim,
    Bitwarped,
    Bitcrusher2,
    Impossible
}

public class PersistentSettings : MonoBehaviour {

    [Header("MainMenu Load Settings")]
    public bool mainMenuVisitedOnGameStartFlag = false;
    public List<string> unlockMessageQueue;

    [Header("Game Load Mode Settings")]
    public GameMode currentModeName;
    public string fireMode;
    public bool rotate45;
    public bool slipperyJumpAllowed;
    public float boostCooldown;
    public bool immuneToCrush;
    public float minWaitTime;
    public float maxWaitTime;
    public bool flipCamera;
    public bool makeScoreNegative;
    public BlockPalette settingPalette;
    public BlockPalette optionOverridePalette;
    public bool impossibleMode;

    [Header("Audio Settings")]
    public bool musicMuted = false;
    public bool sfxMuted = false;

    //Singleton
    static PersistentSettings instance;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
            mainMenuVisitedOnGameStartFlag = false;
            DontDestroyOnLoad(gameObject);
        }

        unlockMessageQueue = new List<string>();
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

    void OnSceneLoaded() {

        Debug.Log("Persistent settings detected that a new scene was loaded.");
    }

    /* Set Default Settings
     * Returns all settings to their defaults for the normal gamemode. 
     * Called my MainMenu before applying other modifiers here and loading the game
     */
    public void SetDefaultSettings() {
        currentModeName = GameMode.Normal;
        fireMode = "tetromino";
        rotate45 = false;
        minWaitTime = 0.4f;
        maxWaitTime = 2.5f;
        flipCamera = false;
        makeScoreNegative = false;
        settingPalette = new BlockPalette();   //default constructor
        slipperyJumpAllowed = false;           //TODO maybe remove?
        boostCooldown = 10.0f;
        impossibleMode = false;
        immuneToCrush = false;
    }

    /* This function is called once upon game scene loading, and upon menu scene loading.
     *  It's not efficient and is pretty bulky, but it's only called once per game load
     *  Note: "GameObject.Find" calls are necessary since scene references can't be set before scene exists
     *  
     *  Game load sets all settings.
     *  Menu load tells menu to go to unlocks screen, but only if the menu has been visited once already (game start). 
     */
    void ApplyGameModeSettings(Scene scene, LoadSceneMode mode) {

        //music obj is same in both scenes
        

        if (scene.name.Equals("Game")) {
            Debug.Log("Persistent settings are applying to the current scene...");

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
            scoreData.currentMode = currentModeName;

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

            AudioSource sceneMusic = GameObject.Find("Music").GetComponent<AudioSource>();
            sceneMusic.mute = musicMuted;

            Debug.Log("Persistent settings application complete.");
        }

        //ensures menu loads into the unlocks screen
        else if (scene.name.Equals("MainMenu")) {

            MainMenu menuNav = GameObject.Find("MenuNavigator").GetComponent<MainMenu>();

            if(mainMenuVisitedOnGameStartFlag && unlockMessageQueue.Count > 0) {
                Debug.Log("PersistentSettings is notifying MenuNavigator that a game has been played. Menu should load into unlock screen.");
                menuNav.userHasPlayedARound = true;
                //there's no need to set the unlock messages; they are collected automatically then wiped by the UnlockCanvas
            }

            if (musicMuted) {
                Debug.Log("Attempting to mute music on main menu");
                menuNav.needToApplyPersistentMusicMute = true;
            }

            if (sfxMuted) {
                Debug.Log("Attempting to mute SFX on main menu.");
                menuNav.needToApplyPersistentSFXMute = true;
            }
        }

        mainMenuVisitedOnGameStartFlag = true; //flag to mark that the main menu has been visited
    }
}
