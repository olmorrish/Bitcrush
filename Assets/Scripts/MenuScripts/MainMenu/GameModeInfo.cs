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
    public GameObject backButton;

    // Start is called before the first frame update
    void Start() {
        infoText.text = "No data.";
    }

    // Update is called once per frame
    void Update() {
        GameObject currentButton = EventSystem.current.currentSelectedGameObject;

        if (currentButton.Equals(normalModeButton))
            infoText.text = "REGULAR BiTCRUSH. TETROMiNOS ONLY. YOU COULD HAVE CLiCKED THiS ON THE MAiN MENU!";
        else if (currentButton.Equals(trominoModeButton))
            infoText.text = "TROMiNOS ONLY. BLOCKS ARE SMALLER, BUT APPEAR MORE OFTEN.";
        else if (currentButton.Equals(pentominoModeButton))
            infoText.text = "PENTOMiNOS ONLY. CLiMBiNG iS MORE DiFFiCULT.";
        else if (currentButton.Equals(bitcrusherModeButton))
            infoText.text = "HARD MODE. DODGE TROMiNOS, TETROMiNOS, AND PENTOMiNOS. BLOCKS APPEAR QUiCKLY.";
        else if (currentButton.Equals(backButton))
            infoText.text = "RETURN TO MAiN MENU.";
        else
            infoText.text = "Selection does not have a description. See GameModeInfo.cs";
    }
}
