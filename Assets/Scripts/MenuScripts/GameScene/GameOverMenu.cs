﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public bool isGameOver = false; 
	private GameObject theGameOverMenu;
	public PauseMenu thePauseMenu;

    public GameObject gameMaster;
    private ScoreData scoreData;
    public GameObject player;
	private Exploder exploder;
    public GameObject musicObject;
	private AudioSource musicTrack;
    private Boost playerBoost;

    // Use this for initialization
    void Start () {
		theGameOverMenu = GameObject.Find("GameOverMenu");                      // Thanks, 2018-me - don't know how this works but it does. Sincerely, 2020-me.
		theGameOverMenu.SetActive(false);

		exploder = player.GetComponent<Exploder>();
        playerBoost = player.GetComponent<Boost>();
        musicTrack = musicObject.GetComponent<AudioSource>();

        scoreData = gameMaster.GetComponent<ScoreData>();

		isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(exploder.exploded && !isGameOver){
			GameOver();
			isGameOver = true;
		}
		
		//don't allow player to pause if on GO screen
		if(isGameOver && thePauseMenu.isPaused){
			thePauseMenu.Resume();
		}
	}
	
	/*
	 * Activates Gameover
	 */
	public void GameOver(){
        //enable the menu
		theGameOverMenu.SetActive(true);
		musicTrack.volume = (0.5f) * musicTrack.volume;
        playerBoost.boostEnabled = false;

        //block unfreeze
        GameObject[] allBlocksInScene = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in allBlocksInScene) {
            block.GetComponent<Freezer>().UnFreeze();
            block.GetComponent<Freezer>().hasntBeenFrozenYet = false;    //stops them from freezing for a first time after gameover
        }

        //highscorescore saves update and 
        scoreData.GameOverSaveHighScore();
        CheckForUnlocksAndQueueMsgs();

    }

    /* Check for Unlocks, Queue Messages
     * Runs through all unlock conditions, the unlocks any that the player has met the requirements for. 
     * If no unlocks were achieved, queues a hint about what to try next instead. 
     */
    private void CheckForUnlocksAndQueueMsgs() {

        PersistentSettings persistentSettings = GameObject.Find("PersistentSettingsObject").GetComponent<PersistentSettings>();

        #region Unlock Checks and Unlock Messages
        //Casual Mode: Unlocks after a single game is played
        if (PlayerPrefs.GetInt("UL_Casual", 0) == 0) {
            PlayerPrefs.SetInt("UL_Casual", 1);
            persistentSettings.unlockMessageQueue.Add("CASUAL MODE UNLOCKED");
        }

        //TODO
        #endregion

        #region Unlock Hints
        if (persistentSettings.unlockMessageQueue.Count < 1) {
            persistentSettings.unlockMessageQueue.Add("GET 200 POiNTS iN NORMAL MODE FOR A NEW GAME MODE");
        }
        #endregion
    }

    /*
	 * Loads scene again
	 */
    public void Retry(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
	
	/*
	 * returns to main menu
	 */
	public void Exit(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

}
