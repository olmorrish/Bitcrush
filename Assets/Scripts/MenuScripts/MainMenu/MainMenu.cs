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
    public GameObject optionsCanvas;
    public Button optionsDefaultButton;
    public GameObject paletteSelectionCanvas;
    public Button paletteSelectionDefaultButton;

    private void Start() {
        settings = settingsObject.GetComponent<PersistentSettings>();
        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(false);
        mainMenuDefaultButton.Select();
    }

    ///////////////////
    /// Menu Navigation
    ///////////////////

    public void ToMainMenuCanvas() {
        Debug.Log("Going to main menu.");
        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        mainMenuDefaultButton.Select();
    }

    public void ToGameModeCanvas() {
        Debug.Log("Going to game modes menu.");
        gameModesCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
        //paletteSelectionCanvas.SetActive(false);
        gameModesDefaultButton.Select();
    }

    public void ToOptionsCanvas() {
        Debug.Log("Going to options menu.");
        mainMenuCanvas.SetActive(false);
        //gameModesCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        paletteSelectionCanvas.SetActive(false);
        optionsDefaultButton.Select();
    }

    public void ToPaletteSelectionCanvas() {
        Debug.Log("Going to palette selection menu.");
        mainMenuCanvas.SetActive(false);
        gameModesCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(true);
        paletteSelectionDefaultButton.Select();
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
        //settings.settingPalette = new BlockPalette("pastel");
        LoadGame();
    }
	
	public void StartPentominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "pentomino";
        LoadGame();
    }

    public void StartBiTCRUSHERGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "all";
        settings.minWaitTime = 0.2f;
        settings.maxWaitTime = 1.75f;
        LoadGame();
    }

    public void StartUniversitySimGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "university";
        settings.minWaitTime = 1f;
        settings.maxWaitTime = 3f;  //let's go easy on 'em
        LoadGame();
    }

    public void StartSlipperySlopesGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.rotate45 = true;
        settings.slipperyJumpAllowed = true;
        settings.settingPalette = new BlockPalette("cool");
        LoadGame();
    }

    public void StartUpsideDownGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.flipCamera = true;
        settings.makeScoreNegative = true;
        LoadGame();
    }

    ///////////////////
    /// Support Methods
    ///////////////////

    public void QuitGame(){
		Application.Quit();
	}

    private void SetDefaultSettings() {
        settings.fireMode = "tetromino";
        settings.rotate45 = false;
        settings.minWaitTime = 0.4f;
        settings.maxWaitTime = 2.5f;
        settings.flipCamera = false;
        settings.makeScoreNegative = false;
        settings.settingPalette = new BlockPalette("pastel");   //default constructor
        settings.slipperyJumpAllowed = false;           //TODO maybe remove?
    }

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
