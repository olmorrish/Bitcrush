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
    //public GameObject optionsCanvas;
    //public Button optionsDefaultButton;
    public GameObject paletteSelectionCanvas;
    public Button paletteSelectionDefaultButton;

    public GameObject palettePreviewHandler;
    private PalettePreview palettePreview;

    [Header("Options Menu References")]
    public GameObject musicObj;
    private AudioSource music;
    private float musicInitialVolume;
    private GameObject postProcessingObj;
    private PostProcessVolume ppVolume;

    [Header("Option Backdrop Colour-Change Vars")]
    private Color green;
    private Color red;
    public GameObject musicToggleBackdropObj;
    private SpriteRenderer musicToggleBackdrop;
    public GameObject sfxToggleBackdropObj;
    private SpriteRenderer sfxToggleBackdrop;
    public GameObject ppToggleBackdropObj;
    private SpriteRenderer ppToggleBackdrop;


    //public GameObject postProcessing;
    //private PostProcessVolume postProcessVolume;
    //private bool postProcessingCurrentlyEnabled;

    private void Start() {

        settingsObject = GameObject.Find("PersistentSettingsObject");                 //because it may be from a previous main menu load
        settings = settingsObject.GetComponent<PersistentSettings>();
        palettePreview = palettePreviewHandler.GetComponent<PalettePreview>();
        //postProcessVolume = postProcessing.GetComponent<PostProcessVolume>();
        //postProcessingCurrentlyEnabled = true;

        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(false);
        mainMenuDefaultButton.Select();

        green = new Color(0f, 1f, 0f, 0.5f);
        red = new Color(1f, 0f, 0f, 0.5f);
        musicToggleBackdrop = musicToggleBackdropObj.GetComponent<SpriteRenderer>();
        sfxToggleBackdrop = sfxToggleBackdropObj.GetComponent<SpriteRenderer>();
        ppToggleBackdrop = ppToggleBackdropObj.GetComponent<SpriteRenderer>();

        music = musicObj.GetComponent<AudioSource>();
        musicInitialVolume = music.volume;
        postProcessingObj = GameObject.Find("PostProcessing");                       //because it may be from a previous main menu load
        ppVolume = postProcessingObj.GetComponent<PostProcessVolume>();
    }

    //buttons can handle most navigation; this is just so that "B" allows the player to go back
    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (gameModesCanvas.activeInHierarchy || paletteSelectionCanvas.activeInHierarchy)
                ToMainMenuCanvas();
        }
    }

    ///////////////////
    /// Menu Navigation
    ///////////////////

    public void ToMainMenuCanvas() {
        Debug.Log("Going to main menu.");
        mainMenuCanvas.SetActive(true);
        gameModesCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(false);
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

    //public void ToOptionsCanvas() {
    //    Debug.Log("Going to options menu.");
    //    mainMenuCanvas.SetActive(false);
    //    //gameModesCanvas.SetActive(false);
    //    optionsCanvas.SetActive(true);
    //    paletteSelectionCanvas.SetActive(false);
    //    optionsDefaultButton.Select();
    //}

    public void ToPaletteSelectionCanvas() {
        Debug.Log("Going to palette selection menu.");
        mainMenuCanvas.SetActive(false);
        gameModesCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
        paletteSelectionCanvas.SetActive(true);
        paletteSelectionDefaultButton.Select();
    }

    ///////////////////
    /// Mode Launchers
    ///////////////////

    public void StartGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();
            //no setting overrides should occur here!
        LoadGame();
    }

    public void StartCasualGame() {
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();
        settings.immuneToCrush = true;
        settings.boostCooldown = 4.5f;
        LoadGame();
    }

    public void StartTrominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "tromino";
        settings.maxWaitTime = 1.5f;
        LoadGame();
    }


    public void StartPentominoGame(){
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "pentomino";
        LoadGame();
    }

    public void StartTinyBlockGame() {
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "tiny";
        settings.maxWaitTime = 1.2f;
        LoadGame();
    }

    public void StartBiTCRUSHERGame() {
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "all";
        settings.minWaitTime = 0.2f;
        settings.maxWaitTime = 1.75f;
        LoadGame();
    }

    public void StartUniversitySimGame() {
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "university";
        settings.minWaitTime = 1f;
        settings.maxWaitTime = 3f;  //let's go easy on 'em
        LoadGame();
    }

    public void StartSlipperySlopesGame() {
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.rotate45 = true;
        settings.slipperyJumpAllowed = true;
        settings.settingPalette = new BlockPalette("mountain");
        LoadGame();
    }

    public void StartUpsideDownGame() {
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.flipCamera = true;
        settings.makeScoreNegative = true;
        LoadGame();
    }

    public void StartBiTWARPEDGame() {
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.fireMode = "warped";
        LoadGame();
    }

    public void StartBiTCRUSHER2Game() {
        //apply the relevant settings to store in the persistent object - prior to loading
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.minWaitTime = 0.1f;
        settings.maxWaitTime = 1.3f;
        settings.boostCooldown = 15.0f;
        LoadGame();
    }

    public void StartImpossibleGame() {
        settings.SetDefaultSettings();   //default - overwrite any others below
        settings.settingPalette = new BlockPalette("black");
        settings.impossibleMode = true; //prevents the player palette from overriding
        LoadGame();
    }

    /////////////////////////////
    /// Option Methods - Palettes
    /////////////////////////////
    
    
    public void PaletteOverride(string paletteName) {

        Debug.Log("Setting block palette to: " + paletteName + ".");
        if(paletteName.Equals("default")){
            settings.optionOverridePalette = null;
            palettePreview.SetPreviewPalette("default");
        }
        else{
            settings.optionOverridePalette = new BlockPalette(paletteName);
            palettePreview.SetPreviewPalette(paletteName);
        }
        //there is no palette override for black since it's only used for a gag in the "impossible mode"
    }

    ///////////////////
    /// Support Methods
    ///////////////////

    public void QuitGame(){
		Application.Quit();
	}

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    /* Toggle Music
     * Switches the music on and off, and also changes its backdrop colour accordingly
     */
    public void ToggleMusic() {
        if (music.volume == 0f) {
            music.volume = musicInitialVolume;
            musicToggleBackdrop.color = green;
        }
        else {
            music.volume = 0f;
            musicToggleBackdrop.color = red;
        }
    }


    public void ToggleSFX() {
        //TODO
    }

    public void TogglePostProcessing() {
        //ppVolume.enabled = !ppVolume.isActiveAndEnabled;

        if (ppVolume.isActiveAndEnabled) {
            ppVolume.enabled = false;
            ppToggleBackdrop.color = red;
        }
        else {
            ppVolume.enabled = true;
            ppToggleBackdrop.color = green;
        }
    }

}
