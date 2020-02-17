using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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

    public GameObject palettePreviewHandler;
    private PalettePreview palettePreview;

    public GameObject postProcessing;
    private PostProcessVolume postProcessVolume;
    private bool postProcessingCurrentlyEnabled;

    private void Start() {
        settings = settingsObject.GetComponent<PersistentSettings>();
        palettePreview = palettePreviewHandler.GetComponent<PalettePreview>();
        postProcessVolume = postProcessing.GetComponent<PostProcessVolume>();
        postProcessingCurrentlyEnabled = true;

        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(false);
        mainMenuDefaultButton.Select();
    }

    //buttons can handle most navigation; this is just so that "B" allows the player to go back
    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (gameModesCanvas.activeInHierarchy || optionsCanvas.activeInHierarchy)
                ToMainMenuCanvas();
            else if (paletteSelectionCanvas.activeInHierarchy)
                ToOptionsCanvas();
            
        }
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
        settings.settingPalette = new BlockPalette("mountain");
        LoadGame();
    }

    public void StartUpsideDownGame() {
        SetDefaultSettings();   //default - overwrite any others below
        settings.flipCamera = true;
        settings.makeScoreNegative = true;
        LoadGame();
    }

    //////////////////
    /// Option Methods
    //////////////////
    
    public void TogglePostProcessing() {
        postProcessVolume.enabled = !postProcessingCurrentlyEnabled;
        postProcessingCurrentlyEnabled = !postProcessingCurrentlyEnabled;
    }


    /////////////////////////////
    /// Option Methods - Palettes
    /////////////////////////////

    public void PaletteOverrideDefault() {
        settings.optionOverridePalette = null;  //will use the default for the gamemode instead
        palettePreview.SetPreviewPalette("default");
    }
    public void PaletteOverridePastel() {
        settings.optionOverridePalette = new BlockPalette("pastel");
        palettePreview.SetPreviewPalette("pastel");
    }
    public void PaletteOverrideWarm() {
        settings.optionOverridePalette = new BlockPalette("warm");
        palettePreview.SetPreviewPalette("warm");
    }
    public void PaletteOverrideCool() {
        settings.optionOverridePalette = new BlockPalette("cool");
        palettePreview.SetPreviewPalette("cool");
    }
    public void PaletteOverrideMonochrome() {
        settings.optionOverridePalette = new BlockPalette("monochrome");
        palettePreview.SetPreviewPalette("monochrome");
    }
    public void PaletteOverridePalewave() {
        settings.optionOverridePalette = new BlockPalette("palewave");
        palettePreview.SetPreviewPalette("palewave");
    }
    //public void PaletteOverrideWhite() {
    //    settings.optionOverridePalette = new BlockPalette("white");
    //    palettePreview.SetPreviewPalette("white");
    //}
    public void PaletteOverrideRetrowave() {
        settings.optionOverridePalette = new BlockPalette("retrowave");
        palettePreview.SetPreviewPalette("retrowave");
    }
    public void PaletteOverride73rfbg0n() {
        settings.optionOverridePalette = new BlockPalette("73rfbg0n");
        palettePreview.SetPreviewPalette("73rfbg0n");
    }
    public void PaletteOverrideMountain() {
        settings.optionOverridePalette = new BlockPalette("mountain");
        palettePreview.SetPreviewPalette("mountain");
    }
    public void PaletteOverrideBeach() {
        settings.optionOverridePalette = new BlockPalette("beach");
        palettePreview.SetPreviewPalette("beach");
    }
    public void PaletteOverrideForest() {
        settings.optionOverridePalette = new BlockPalette("forest");
        palettePreview.SetPreviewPalette("forest");
    }
    public void PaletteOverrideRandom() {
        settings.optionOverridePalette = new BlockPalette("random");
        palettePreview.SetPreviewPalette("random");
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
        settings.settingPalette = new BlockPalette();   //default constructor
        settings.slipperyJumpAllowed = false;           //TODO maybe remove?
    }

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
