using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* PlayerProgress
 * This script deals with any unlocks or changes made the the menu based on PlayerPrefs variables.
 */
public class PlayerProgress : MonoBehaviour {

    [Header("GameMode Menu Buttons")]
    public Button casualButton;
    public Button trominoButton;
    public Button pentominoButton;
    public Button tinyButton;
    public Button bitcrusherButton;
    public Button slipperySlopesButton;
    public Button upsideDownButton;
    public Button universitySimButton;
    public Button bitwarpedButton;
    public Button bitcrusher2Button;
    public Button impossibleButton;

    [Header("Highscore Menu Text Objs")]
    public GameObject casualHSText;
    public GameObject trominoHSText;
    public GameObject pentominoHSText;
    public GameObject tinyHSText;
    public GameObject bitcrusherHSText;
    public GameObject slipperySlopesHSText;
    public GameObject upsideDownHSText;
    public GameObject universitySimHSText;
    public GameObject bitwarpedHSText;
    public GameObject bitcrusher2HSText;
    //public GameObject impossibleHSText;

    [Header("Palette Menu Buttons")]
    public Button DefaultButton;
    public Button PastelButton;
    public Button WarmButton;
    public Button CoolButton;
    public Button MonochromeButton;
    public Button PalewaveButton;
    public Button RetrowaveButton;
    public Button PrideButton;
    public Button MountainButton;
    public Button BeachButton;
    public Button ForestButton;
    public Button RandomButton;


    // Start is called before the first frame update
    void Start() {

        #region GameMode Unlocks
        if(PlayerPrefs.GetInt("UL_Casual", 0) == 0) {
            casualButton.interactable = false;
            casualButton.GetComponentInChildren<Text>().text = "???";
            casualHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Tromino", 0) == 0) {
            trominoButton.interactable = false;
            trominoButton.GetComponentInChildren<Text>().text = "???";
            trominoHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Pentomino", 0) == 0) {
            pentominoButton.interactable = false;
            pentominoButton.GetComponentInChildren<Text>().text = "???";
            pentominoHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Tiny", 0) == 0) {
            tinyButton.interactable = false;
            tinyButton.GetComponentInChildren<Text>().text = "???";
            tinyHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Bitcrusher", 0) == 0) {
            bitcrusherButton.interactable = false;
            bitcrusherButton.GetComponentInChildren<Text>().text = "???";
            bitcrusherHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_SlipperySlopes", 0) == 0) {
            slipperySlopesButton.interactable = false;
            slipperySlopesButton.GetComponentInChildren<Text>().text = "???";
            slipperySlopesHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_UpsideDown", 0) == 0) {
            upsideDownButton.interactable = false;
            upsideDownButton.GetComponentInChildren<Text>().text = "???";
            upsideDownHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_UniversitySim", 0) == 0) {
            universitySimButton.interactable = false;
            universitySimButton.GetComponentInChildren<Text>().text = "???";
            universitySimHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Bitwarped", 0) == 0) {
            bitwarpedButton.interactable = false;
            bitwarpedButton.GetComponentInChildren<Text>().text = "???";
            bitwarpedHSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Bitcrusher2", 0) == 0) {
            bitcrusher2Button.interactable = false;
            bitcrusher2Button.GetComponentInChildren<Text>().text = "???";
            bitcrusher2HSText.GetComponent<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("UL_Impossible", 0) == 0) {
            impossibleButton.interactable = false;
            impossibleButton.GetComponentInChildren<Text>().text = "???";
        }

        #endregion
        #region PaletteUnlocks

        if (PlayerPrefs.GetInt("PAL_Pastel", 0) == 0) {
            PastelButton.interactable = false;
            PastelButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Warm", 0) == 0) {
            WarmButton.interactable = false;
            WarmButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Cool", 0) == 0) {
            CoolButton.interactable = false;
            CoolButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Monochrome", 0) == 0) {
            MonochromeButton.interactable = false;
            MonochromeButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Palewave", 0) == 0) {
            PalewaveButton.interactable = false;
            PalewaveButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Retrowave", 0) == 0) {
            RetrowaveButton.interactable = false;
            RetrowaveButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Pride", 0) == 0) {
            PrideButton.interactable = false;
            PrideButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Mountain", 0) == 0) {
            MountainButton.interactable = false;
            MountainButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Beach", 0) == 0) {
            BeachButton.interactable = false;
            BeachButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Forest", 0) == 0) {
            ForestButton.interactable = false;
            ForestButton.GetComponentInChildren<Text>().text = "???";
        }

        if (PlayerPrefs.GetInt("PAL_Random", 0) == 0) {
            RandomButton.interactable = false;
            RandomButton.GetComponentInChildren<Text>().text = "???";
        }

        #endregion

    }
}
