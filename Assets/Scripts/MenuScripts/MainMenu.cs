using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	public void StartGame(){
		SceneManager.LoadScene("NormalMode", LoadSceneMode.Single);
	}
	
	public void StartTrominoGame(){
		//SceneManager.LoadScene("Game_Tromino", LoadSceneMode.Single);
	}
	
	public void StartPentominoGame(){
		//SceneManager.LoadScene("Game_Pentomino", LoadSceneMode.Single);
	}
	
	public void QuitGame(){
		Application.Quit();
	}
}
