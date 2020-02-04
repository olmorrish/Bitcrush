using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public bool isPaused = false; 
	public GameObject pauseMenuUI; 

	// Use this for initialization
	void Awake () {
		pauseMenuUI = GameObject.Find("PauseMenu");
		pauseMenuUI.SetActive(false);
		Resume();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start")){
			if(isPaused){
				Resume();
			}
			else{
				Pause();
			}
		}
	}
	
	public void Resume(){
		
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}
	
	
	public void Pause(){
		
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	
	public void Exit(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
	
	
}
