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
        SetDefaultSettings();
            //no setting overrides should occur here!
        LoadGame();
    }
	
	public void StartTrominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "tromino";
        settings.maxWaitTime = 1.5f;
        LoadGame();
    }
	
	public void StartPentominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "pentomino";
        LoadGame();
    }
	
	public void QuitGame(){
		Application.Quit();
	}

    private void SetDefaultSettings() {
        settings.fireMode = "tetromino";
        settings.rotate45 = false;
        settings.minWaitTime = 0.4f;
        settings.maxWaitTime = 2.5f;
    }

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
