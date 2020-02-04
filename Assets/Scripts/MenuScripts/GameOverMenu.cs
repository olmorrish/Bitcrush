using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public bool go = false; 
	public GameObject goMenuUI;
	private PauseMenu pauseMenu;
	private PlayerExplodesIntoPixels exp;
	private AudioSource music;
	
	private Text mainGameScore;
	private Text postGameScore;

	// Use this for initialization
	void Start () {
		goMenuUI = GameObject.Find("GameOverMenu");
		goMenuUI.SetActive(false);
		exp = GameObject.Find("Player").GetComponent<PlayerExplodesIntoPixels>();
		pauseMenu = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();
		music = GameObject.Find("Music").GetComponent<AudioSource>();
		
		mainGameScore = GameObject.Find("ScoreText").GetComponent<Text>();
		
		go = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(exp.exploded && !go){
			GameOver();
			go = true;
		}
		
		//don't allow player to pause if on GO screen
		if(go && pauseMenu.isPaused){
			pauseMenu.Resume();
		}
	}
	
	/*
	 * Activates Gameover
	 */
	public void GameOver(){
		goMenuUI.SetActive(true);
		
		postGameScore = GameObject.Find("FinalScore").GetComponent<Text>();	
		postGameScore.text = "FiNAL " + mainGameScore.text;
		//Time.timeScale = 0f;
		
		music.volume = (0.5f) * music.volume;
	}
	
	/*
	 * Activates Gameover
	 */
	public void Retry(){
		goMenuUI.SetActive(false);
		//Time.timeScale = 1f;
		go = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
	
	/*
	 * returns to main menu
	 */
	public void Exit(){
		go = false;
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
