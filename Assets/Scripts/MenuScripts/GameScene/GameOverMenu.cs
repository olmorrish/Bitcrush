using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public bool isGameOver = false; 
	private GameObject theGameOverMenu;
	public PauseMenu thePauseMenu;

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
		theGameOverMenu.SetActive(true);
		musicTrack.volume = (0.5f) * musicTrack.volume;

        playerBoost.boostEnabled = false;

        GameObject[] allBlocksInScene = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in allBlocksInScene) {
            block.GetComponent<Freezer>().UnFreeze();
            block.GetComponent<Freezer>().hasntBeenFrozenYet = false;    //stops them from freezing for a first time after gameover
        }
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
