using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public bool isPaused = false; 
	public GameObject thePauseMenu;

    public GameObject player;
    private Boost playerBoost;

	// Use this for initialization
	void Awake () {
		thePauseMenu = GameObject.Find("PauseMenu");
		thePauseMenu.SetActive(false);
		Resume();

        playerBoost = player.GetComponent<Boost>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start")){
			if(isPaused){
				Resume();
			}
			else{
				Pause();
			}
		}
	}
	
	public void Resume(){
		
		thePauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
        //playerBoost.boostEnabled = true;
    }

    public void Retry() {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        //playerBoost.boostEnabled = true;
    }

    public void Pause(){
		
		thePauseMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
        playerBoost.boostEnabled = false;

    }

	
	public void Exit(){
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
	
	
}
