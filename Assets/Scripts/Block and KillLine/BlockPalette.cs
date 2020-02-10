using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPalette {

    public Color[] palette = new Color[8];

    //default constructor
    public BlockPalette() {
        palette[0] = new Color(0, 1, 0, 255);           //green
        palette[1] = new Color(1, 0, 1, 255);           //pink
        palette[2] = new Color(1, 16f/255f, 0, 255);    //red
        palette[3] = new Color(0, 128f/255f, 1, 255);   //blue
        palette[4] = new Color(1, 1, 0, 255);           //yellow
        palette[5] = new Color(1, 102f/255f, 0, 255);   //orange
        palette[6] = new Color(0, 1, 191f/255f, 255);   //teal
        palette[7] = new Color(191f/255f, 0, 1, 255);   //purple
    }

    //string arg constructor
    public BlockPalette(string paletteMode) {
        if (paletteMode.Equals("white")) {
            for (int i = 0; i < 8; i++) {
                palette[i] = new Color(1, 1, 1, 255);
            }
        }

        else if (paletteMode.Equals("black")) {     //lmao
            for (int i = 0; i < 8; i++) {
                palette[i] = new Color(0, 0, 0, 255);
            }
        }

        else if (paletteMode.Equals("fullrandom")) {     //oh no
            for (int i = 0; i < 8; i++) {
                palette[i] = new Color((Random.Range(0, 255) / 255f), (Random.Range(0, 255) / 255f), (Random.Range(0, 255) / 255f), 255);
            }
        }

        else if (paletteMode.Equals("pseudorandom")) {     //oh no
            for (int i = 0; i < 8; i++) {
                palette[i] = new Color((Random.Range(20, 255) / 255f), (Random.Range(20, 255) / 255f), (Random.Range(20, 255) / 255f), 255);
            }
        }

        else if (paletteMode.Equals("greyscale")) {
            palette[0] = new Color(229f / 255f, 229f / 255f, 229f / 255f, 255);    //greys
            palette[1] = new Color(191f / 255f, 191f / 255f, 191f / 255f, 255);
            palette[2] = new Color(153f / 255f, 153f / 255f, 153f / 255f, 255);
            palette[3] = new Color(102f / 255f, 102f / 255f, 102f / 255f, 255);
            palette[4] = new Color(63f / 255f, 63f / 255f, 63f / 255f, 255);
            palette[5] = new Color(38f / 255f, 38f / 255f, 38f / 255f, 255);
            palette[6] = new Color(1, 1, 1, 255);    //white
            palette[7] = new Color(1, 1, 1, 255);
        }

        else if (paletteMode.Equals("hot")) {
            palette[0] = new Color(252f / 255f, 252f / 255f, 12f / 255f, 255);     //yellow
            palette[1] = new Color(252f / 255f, 200f, 12f / 255f, 255);
            palette[2] = new Color(252f / 255f, 156f / 255f, 12f / 255f, 255);
            palette[3] = new Color(252f / 255f, 124f / 255f, 12f / 255f, 255);     //orange
            palette[4] = new Color(252f / 255f, 104f / 255f, 12f / 255f, 255);
            palette[5] = new Color(252f / 255f, 84f / 255f, 12f / 255f, 255);
            palette[6] = new Color(252f / 255f, 60f, 12f / 255f, 255);
            palette[7] = new Color(252f / 255f, 12f / 255f, 12f / 255f, 255);      //red
        }

        else if (paletteMode.Equals("cool")) {
            palette[0] = new Color(92f / 255f, 252f / 255f, 12f / 255f, 255);     //green
            palette[1] = new Color(12f / 255f, 252f / 255f, 32f / 255f, 255);
            palette[2] = new Color(12f / 255f, 252f / 255f, 160f, 255);
            palette[3] = new Color(12f / 255f, 244f / 255f, 252f / 255f, 255);     //blue
            palette[4] = new Color(12f / 255f, 184f / 255f, 252f / 255f, 255);
            palette[5] = new Color(12f / 255f, 136f / 255f, 252f / 255f, 255);
            palette[6] = new Color(57f / 255f, 80f, 229f / 255f, 255);      //dark blue
            palette[7] = new Color(117f / 255f, 57f / 255f, 229f / 255f, 255);     //purple      
        }

        else if (paletteMode.Equals("pastel")) {
            palette[0] = new Color(1, 127f / 255f, 127f / 255f, 255);     //roy
            palette[1] = new Color(1, 207f / 255f, 127f / 255f, 255);
            palette[2] = new Color(239f / 255f, 1, 127f / 255f, 255);
            palette[3] = new Color(127f / 255f, 1, 178f / 255f, 255);     //G
            palette[4] = new Color(127f / 255f, 233f / 255f, 1, 255);     //biv
            palette[5] = new Color(127f / 255f, 136f / 255f, 1, 255);
            palette[6] = new Color(229f / 255f, 127f / 255f, 1, 255);
            palette[7] = new Color(1, 127f / 255f, 208f / 255f, 255);     //pink!
        }

        else
            Debug.LogError("Attempted to designate invalid BlockPalette palette.");
    }
}
