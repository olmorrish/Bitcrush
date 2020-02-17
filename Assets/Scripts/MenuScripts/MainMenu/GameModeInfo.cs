using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameModeInfo : MonoBehaviour {

    public Text infoText;
    public GameObject normalModeButton;
    public GameObject trominoModeButton;
    public GameObject pentominoModeButton;
    public GameObject bitcrusherModeButton;

    public GameObject universitySimButton;
    public GameObject slipperySlopesButton;
    public GameObject upsideDownButton;

    public GameObject backButton;

    // Start is called before the first frame update
    void Start() {
        infoText.text = "No data.";
    }

    // Update is called once per frame
    void Update() {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;

        if (currentButton.Equals(normalModeButton))
            infoText.text = "REGULAR BiTCRUSH! TETROMiNOES ONLY. YOU COULD HAVE SELECTED THiS ON THE MAiN MENU!";
        else if (currentButton.Equals(trominoModeButton))
            infoText.text = "TROMiNOES ONLY. BLOCKS ARE SMALLER, BUT APPEAR MORE OFTEN.";
        else if (currentButton.Equals(pentominoModeButton))
            infoText.text = "PENTOMiNOES ONLY. BLOCKS ARE LARGER AND CLiMBiNG iS MORE DiFFiCULT.";
        else if (currentButton.Equals(bitcrusherModeButton))
            infoText.text = "HARD MODE! " +
                "\n\nDODGE ALL THREE MAiN BLOCK TYPES: TROMiNOES, TETROMiNOES, AND PENTOMiNOES. " +
                "\n\nBLOCKS APPEAR QUiCKLY.";
        else if (currentButton.Equals(universitySimButton))
            infoText.text = "THiS GAMEMODE iS A POiGNANT CRiTiQUE OF THE EDUCATiON SYSTEM.";
        else if (currentButton.Equals(slipperySlopesButton))
            infoText.text = "BLOCKS ARE ROTATED BY 45 DEGREES." +
                "\n\nWATCH YOUR STEP!";
        else if (currentButton.Equals(upsideDownButton))
            infoText.text = "THE ONLY WAY TO GET LESS THAN ZERO POiNTS." +
                "\n\nLEFT/RiGHT CONTROLS ARE iNVERTED.";
        else if (currentButton.Equals(backButton))
            infoText.text = "RETURN TO MAiN MENU.";
        else
            infoText.text = "Gamemode info text is not available.";
    }
}
