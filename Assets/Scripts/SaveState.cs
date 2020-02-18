using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState {

    //Colour Palette Unlocks
    public bool hasPal_Default;
    public bool hasPal_Pastel;
    public bool hasPal_Warm;
    public bool hasPal_Cool;
    public bool hasPal_Monochrome;
    public bool hasPal_Palewave;
    public bool hasPal_Retrowave;
    public bool hasPal_73rfbg0n;
    public bool hasPal_Mountain;
    public bool hasPal_Beach;
    public bool hasPal_Forest;
    public bool hasPal_Random;

    //Game Mode Unlocks
    public bool hasMode_Normal;
    public bool hasMode_Tromino;
    public bool hasMode_Pentomino;
    public bool hasMode_BiTCRUSHER;
    public bool hasMode_UniversitySimulator;
    public bool hasMode_SlipperySlopes;
    public bool hasMode_UpsideDown;

    //Local Highscores
    public bool hiScore_Normal;
    public bool hiScore_Tromino;
    public bool hiScore_Pentomino;
    public bool hiScore_BiTCRUSHER;
    public bool hiScore_UniversitySimulator;
    public bool hiScore_SlipperySlopes;
    public bool hiScore_UpsideDown;


    public SaveState() {

    }

}
