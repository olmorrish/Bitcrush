﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public bool isGameOver = false; 
	private GameObject theGameOverMenu;
	private PauseMenu thePauseMenu;

    public GameObject player;
	private PlayerExplodesIntoPixels exploder;
    public GameObject musicObject;
	private AudioSource musicTrack;

	// Use this for initialization
	void Start () {
		theGameOverMenu = GameObject.Find("GameOverMenu");                      //thanks, past me - don't know how this works but it does
		thePauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();
		theGameOverMenu.SetActive(false);

		exploder = player.GetComponent<PlayerExplodesIntoPixels>();
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
		//Time.timeScale = 0f;
	}
	
	/*
	 * Activates Gameover
	 */
	public void Retry(){
		theGameOverMenu.SetActive(false);
		//Time.timeScale = 1f;
		isGameOver = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
	
	/*
	 * returns to main menu
	 */
	public void Exit(){
		isGameOver = false;
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
