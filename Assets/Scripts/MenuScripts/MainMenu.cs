using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject settingsObject;
    private PersistentSettings settings;

    public GameObject mainMenuCanvas;
    public Button mainMenuDefaultButton;
    public GameObject gameModesCanvas;
    public Button gameModesDefaultButton;
    public GameObject customGamesCanvas;
    public Button customGamesDefaultButton;

    private void Start() {
        settings = settingsObject.GetComponent<PersistentSettings>();
        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        customGamesCanvas.SetActive(false);
        mainMenuDefaultButton.Select();
    }

    ///////////////////
    /// Menu Navigation
    ///////////////////

    public void ToMainMenuCanvas() {
        Debug.Log("Going to main menu.");
        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        customGamesCanvas.SetActive(false);
        mainMenuDefaultButton.Select();
    }

    public void ToGameModeCanvas() {
        Debug.Log("Going to game modes menu.");
        gameModesCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        customGamesCanvas.SetActive(false);
        gameModesDefaultButton.Select();
    }

    public void ToCustomGamesCanvas() {
        Debug.Log("Going to custom games menu.");
        mainMenuCanvas.SetActive(false);
        gameModesCanvas.SetActive(false);
        customGamesCanvas.SetActive(true);
        customGamesDefaultButton.Select();
    }

    ///////////////////
    /// Mode Launchers
    ///////////////////
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

    public void StartBiTCHRUSHERGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "all";
        settings.minWaitTime = 0.2f;
        settings.maxWaitTime = 1.75f;
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
