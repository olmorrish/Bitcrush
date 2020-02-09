using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject settingsObject;
    private PersistentSettings settings;

    private void Start() {
        settings = settingsObject.GetComponent<PersistentSettings>();
    }

    public void StartGame(){
		//apply the relevant settings to store in the persistent object - prior to loading
        settings.fireMode = "tetromino";
        LoadGame();
    }
	
	public void StartTrominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.fireMode = "tromino";
        LoadGame();
    }
	
	public void StartPentominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.fireMode = "pentomino";
        LoadGame();
    }
	
	public void QuitGame(){
		Application.Quit();
	}

    private void SetDefaultSettings() {
        settings.rotate45 = false;
    }

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
