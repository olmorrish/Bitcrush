using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameModeInfo : MonoBehaviour {

    public Text infoText;
    public GameObject normalModeButton;
    public GameObject casualModeButton;
    public GameObject trominoModeButton;
    public GameObject pentominoModeButton;
    public GameObject tinyBlocksButton;

    public GameObject bitcrusherModeButton;
    public GameObject slipperySlopesButton;
    public GameObject upsideDownButton;
    public GameObject universitySimButton;
    public GameObject bitwarpedModeButton;
    public GameObject bitcrusher2Button;
    public GameObject impossibleModeButton;

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
        else if (currentButton.Equals(casualModeButton))
            infoText.text = "FALLiNG BLOCKS CAN NO LONGER HURT YOU AND APPEAR LESS OFTEN." +
                "\n\nBOOST COOLDOWN iS REDUCED." +
                "\n\nGOOD FOR PRACTiCE.";
        else if (currentButton.Equals(trominoModeButton))
            infoText.text = "TROMiNOES ONLY. BLOCKS ARE SMALLER, BUT APPEAR MORE OFTEN.";
        else if (currentButton.Equals(pentominoModeButton))
            infoText.text = "PENTOMiNOES ONLY. BLOCKS ARE LARGER AND CLiMBiNG iS MORE DiFFiCULT.";
        else if (currentButton.Equals(tinyBlocksButton))
            infoText.text = "DOMiNOES AND \"MONOMiNOES\" (LiTERAL SQUARES) RAiN FROM ABOVE." +
                "\n\nBLOCKS APPEAR RAPiDLY AND ARE DiFFiCULT TO DODGE.";
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
        else if (currentButton.Equals(bitwarpedModeButton))
            infoText.text = "BLOCKS ARE MASSIVE AND ARE ... ODD SHAPES. " +
                "\n\nTECHNiCALLY, EVERY BLOCK iN THIS MODE iS AN ORTHOGONAL PERMUTATiON, AND SO THEY'RE STiLL \"POLYMiNOES\"!";
        else if (currentButton.Equals(bitcrusher2Button))
            infoText.text = "THOUGHT BiTCRUSHER MODE WAS HARD?" +
                "\n\nDODGE ALL THREE MAiN BLOCK TYPES: TROMiNOES, TETROMiNOES, AND PENTOMiNOES. BLOCKS APPEAR EVEN FASTER THAN REGULAR BiTCRUSHER MODE!" +
                "\n\nBOOST COOLDOWN iS iNCREASED.";
        else if (currentButton.Equals(impossibleModeButton))
            infoText.text = "THiS MODE iS 100% A JOKE." +
                "\n\nSERiOUSLY, i'M NOT EVEN TRACKiNG YOUR HiGHSCORE FOR THiS ONE.";
        else if (currentButton.Equals(backButton))
            infoText.text = "RETURN TO MAiN MENU.";
        else
            infoText.text = "Gamemode info text is not available.";
    }
}
