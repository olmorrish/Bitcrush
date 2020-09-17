using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightRetryIfGameOver : MonoBehaviour {

	public Button retryButton;
	private GameOverMenu gameOverMenu;
	private bool retrySet = false;

	// Use this for initialization
	void Start () {
		gameOverMenu = GetComponent<GameOverMenu>();
		retrySet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOverMenu.isGameOver && !retrySet){
			retryButton.Select();
			retrySet = true;
		}
		else if(!gameOverMenu.isGameOver){
			retrySet = false;
		}
	}
}
