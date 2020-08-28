﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour {

    public bool resetAllProgress = false;
    public bool unlockEverything = false;

    // Start is called before the first frame update
    void Awake(){
        
        if (resetAllProgress) {
            PlayerPrefs.DeleteAll();
        }
        else if (unlockEverything) {
            PlayerPrefs.SetInt("UL_Casual", 1);
            PlayerPrefs.SetInt("UL_Tromino", 1);
            PlayerPrefs.SetInt("UL_Pentomino", 1);
            PlayerPrefs.SetInt("UL_Tiny", 1);
            PlayerPrefs.SetInt("UL_Bitcrusher", 1);
            PlayerPrefs.SetInt("UL_UniversitySim", 1);
            PlayerPrefs.SetInt("UL_Bitcrusher2", 1);
            PlayerPrefs.SetInt("UL_Bitwarped", 1);
            PlayerPrefs.SetInt("UL_Impossible", 1);
            PlayerPrefs.SetInt("UL_SlipperySlopes", 1);
            PlayerPrefs.SetInt("UL_UpsideDown", 1);
        }

    }

    // Update is called once per frame
    void Update() {


    }
}
