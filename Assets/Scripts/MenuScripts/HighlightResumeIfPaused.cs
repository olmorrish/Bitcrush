using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightResumeIfPaused : MonoBehaviour {

	public Button resumeButton;
	private PauseMenu pauseMenu;
	private GameOverMenu goMenu;
	private bool resumeSet = false;

	

	// Use this for initialization
	void Start () {
		pauseMenu = GetComponent<PauseMenu>();
		goMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenu>();
		resumeSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(pauseMenu.isPaused && !goMenu.isGameOver && !resumeSet){
			resumeButton.Select();
			resumeSet = true;
		}
		else if(!pauseMenu.isPaused){
			resumeSet = false;
		}
	}
}
