using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {

    [Header ("HighScores")]
    public int HS_Normal;
    public int HS_Casual;
    public int HS_Tromino;
    public int HS_Pentomino;
    public int HS_Tiny;
    public int HS_Bitcrusher;
    public int HS_SlipperySlopes;
    public int HS_UpsideDown;
    public int HS_UniversitySim;
    public int HS_Bitwarped;
    public int HS_Bitcrusher2;

    // Start is called before the first frame update
    void Start() {
        HS_Normal = PlayerPrefs.GetInt("HS_Normal", 0);
        HS_Casual = PlayerPrefs.GetInt("HS_Casual", 0);
        HS_Tromino = PlayerPrefs.GetInt("HS_Tromino", 0);
        HS_Pentomino = PlayerPrefs.GetInt("HS_Pentomino", 0);
        HS_Tiny = PlayerPrefs.GetInt("HS_Tiny", 0);
        HS_Bitcrusher = PlayerPrefs.GetInt("HS_Bitcrusher", 0);
        HS_SlipperySlopes = PlayerPrefs.GetInt("HS_SlipperySlopes", 0);
        HS_UpsideDown = PlayerPrefs.GetInt("HS_UpsideDown", 0);
        HS_UniversitySim = PlayerPrefs.GetInt("HS_UniversitySim", 0);
        HS_Bitwarped = PlayerPrefs.GetInt("HS_Bitwarped", 0);
        HS_Bitcrusher2 = PlayerPrefs.GetInt("HS_Bitcrusher2", 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
