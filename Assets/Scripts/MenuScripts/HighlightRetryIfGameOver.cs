using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightRetryIfGameOver : MonoBehaviour {

	public Button retryButton;
	private GameOverMenu goMenu;
	private bool retrySet = false;

	// Use this for initialization
	void Start () {
		goMenu = GetComponent<GameOverMenu>();
		retrySet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(goMenu.go && !retrySet){
			retryButton.Select();
			retrySet = true;
		}
		else if(!goMenu.go){
			retrySet = false;
		}
	}
}
