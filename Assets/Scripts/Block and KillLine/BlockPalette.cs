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

        if (paletteMode.Equals("default")) {
            palette = new BlockPalette().palette;
        }

        //else if (paletteMode.Equals("black")) {     //lmao
        //    for (int i = 0; i < 8; i++) {
        //        palette[i] = new Color(0, 0, 0, 255);
        //    }
        //}

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

        else if (paletteMode.Equals("warm")) {
            palette[0] = new Color(252f / 255f, 252f / 255f, 12f / 255f, 255);     //yellow
            palette[1] = new Color(252f / 255f, 200f / 255f, 12f / 255f, 255);
            palette[2] = new Color(252f / 255f, 156f / 255f, 12f / 255f, 255);
            palette[3] = new Color(252f / 255f, 124f / 255f, 12f / 255f, 255);     //orange
            palette[4] = new Color(252f / 255f, 104f / 255f, 12f / 255f, 255);
            palette[5] = new Color(252f / 255f, 84f / 255f, 12f / 255f, 255);
            palette[6] = new Color(252f / 255f, 60f / 255f, 12f / 255f, 255);
            palette[7] = new Color(252f / 255f, 12f / 255f, 12f / 255f, 255);      //red
        }

        else if (paletteMode.Equals("cool")) {
            palette[0] = new Color(92f / 255f, 252f / 255f, 12f / 255f, 255);     //green
            palette[1] = new Color(12f / 255f, 252f / 255f, 32f / 255f, 255);
            palette[2] = new Color(12f / 255f, 252f / 255f, 160f / 255f, 255);
            palette[3] = new Color(12f / 255f, 244f / 255f, 252f / 255f, 255);     //blue
            palette[4] = new Color(12f / 255f, 184f / 255f, 252f / 255f, 255);
            palette[5] = new Color(12f / 255f, 136f / 255f, 252f / 255f, 255);
            palette[6] = new Color(57f / 255f, 80f / 255f, 229f / 255f, 255);      //dark blue
            palette[7] = new Color(117f / 255f, 57f / 255f, 229f / 255f, 255);     //purple      
        }

        else if (paletteMode.Equals("monochrome")) {
            palette[0] = new Color(229f / 255f, 229f / 255f, 229f / 255f, 255);    //greys
            palette[1] = new Color(191f / 255f, 191f / 255f, 191f / 255f, 255);
            palette[2] = new Color(153f / 255f, 153f / 255f, 153f / 255f, 255);
            palette[3] = new Color(102f / 255f, 102f / 255f, 102f / 255f, 255);
            palette[4] = new Color(63f / 255f, 63f / 255f, 63f / 255f, 255);
            palette[5] = new Color(38f / 255f, 38f / 255f, 38f / 255f, 255);
            palette[6] = new Color(1, 1, 1, 255);    //white
            palette[7] = new Color(1, 1, 1, 255);
        }

        else if (paletteMode.Equals("palewave")) {
            palette[0] = new Color(200f / 255f, 255f / 255f, 252f / 255f, 255); 
            palette[1] = new Color(200f / 255f, 255f / 255f, 245f / 255f, 255);
            palette[2] = new Color(224f / 255f, 255f / 255f, 200f / 255f, 255);
            palette[3] = new Color(243f / 255f, 255f / 255f, 200f / 255f, 255);
            palette[4] = new Color(200f / 255f, 239f / 255f, 255f / 255f, 255);
            palette[5] = new Color(200f / 255f, 224f / 255f, 255f / 255f, 255);
            palette[6] = new Color(255f / 255f, 200f / 255f, 252f / 255f, 255);
            palette[7] = new Color(255f / 255f, 255f / 255f, 200f / 255f, 255);
        }

        //else if (paletteMode.Equals("white")) {
        //    for (int i = 0; i < 8; i++) {
        //        palette[i] = new Color(1, 1, 1, 255);
        //    }
        //}

        else if (paletteMode.Equals("retrowave")) {
            palette[0] = new Color(252f / 255f, 200f / 252f, 12f / 255f, 255);  //yellow
            palette[1] = new Color(255f / 255f, 67f / 255f, 101f / 255f, 255);  //off-red
            palette[2] = new Color(84f / 255f, 13f / 255f, 110f / 255f, 255);   //deep purple
            palette[3] = new Color(121f / 255f, 30f / 255f, 148f / 255f, 255);  //purple
            palette[4] = new Color(84f / 255f, 19f / 255f, 136f / 255f, 255);   //blued purple
            palette[5] = new Color(212f / 255f, 0, 120f / 255f, 255);           //magenta
            palette[6] = new Color(246f / 255f, 1f / 255f, 157f / 255f, 255);   //hot pink
            palette[7] = new Color(1, 108f / 255f, 17f / 255f, 255);            //orange
        }

        else if (paletteMode.Equals("73rfbg0n")) {
            palette[0] = new Color(255f / 255f, 127f / 255f, 208f / 255f, 255); //pinks
            palette[1] = new Color(255f / 255f, 89f / 255f, 194f / 255f, 255);
            palette[2] = new Color(255f / 255f, 50f / 255f, 180f / 255f, 255);
            palette[3] = new Color(255f / 255f, 127f / 255f, 148f / 255f, 255);
            palette[4] = new Color(127f / 255f, 233f / 255f, 255f / 255f, 255); //blues
            palette[5] = new Color(127f / 255f, 176f / 255f, 1, 255);
            palette[6] = new Color(57f / 255f, 180f / 255f, 229f / 255f, 255);
            palette[7] = new Color(50f / 255f, 220f / 255f, 1, 255);
        }

        else if (paletteMode.Equals("mountain")) {
            palette[0] = new Color(101f / 255f, 178f / 255f, 62f / 255f, 255);    //green
            palette[1] = new Color(60f / 255f, 165f / 255f, 8f / 255f, 255);
            palette[2] = new Color(127f / 255f, 233f / 255f, 255f / 255f, 255);   //light blue
            palette[3] = new Color(89f / 255f, 227f / 255f, 255f / 255f, 255);
            palette[4] = new Color(50f / 255f, 220f / 255f, 255f / 255f, 255);
            palette[5] = new Color(167f / 255f, 167f / 255f, 167f / 255f, 255);   //grey
            palette[6] = new Color(191f / 255f, 191f / 255f, 191f / 255f, 255);
            palette[7] = new Color(127f / 255f, 176f / 255f, 255f / 255f, 255);   //blue again     
        }

        else if (paletteMode.Equals("beach")) {
            palette[0] = new Color(57f / 255f, 229f / 255f, 163f / 255f, 255); //seafoam
            palette[1] = new Color(12f / 255f, 184f / 255f, 252f / 255f, 255); //teal
            palette[2] = new Color(225f / 255f, 159f / 255f, 0, 255);          //very orange
            palette[3] = new Color(57f / 255f, 180f / 255f, 129f / 255f, 255); //teal
            palette[4] = new Color(229f / 255f, 160f / 255f, 57f / 255f, 255); //dull orange
            palette[5] = new Color(229f / 255f, 192f / 255f, 57f / 255f, 255); //dull orange
            palette[6] = new Color(57f / 255f, 146f / 255f, 229f / 255f, 255); //blue
            palette[7] = new Color(127f / 255f, 176f / 255f, 255f / 255f, 255);//purpleish
        }

        else if (paletteMode.Equals("forest")) {
            palette[0] = new Color(101f / 255f, 178f / 255f, 62f / 255f, 255); //greens
            palette[1] = new Color(62f / 255f, 178f / 255f, 72f / 255f, 255);
            palette[2] = new Color(114f / 255f, 229f / 255f, 57f / 255f, 255);
            palette[3] = new Color(57f / 255f, 229f / 255f, 71f / 255f, 255);
            palette[4] = new Color(178f / 255f, 132f / 255f, 62f / 255f, 255);       //browns
            palette[5] = new Color(229f / 255f, 160f / 255f, 57f / 255f, 255);
            palette[6] = new Color(229f / 255f, 57f / 255f, 57f / 255f, 255);       //red
            palette[7] = new Color(229f / 255f, 57f / 255f, 117f / 255f, 255);           //passionred
        }

        else if (paletteMode.Equals("random")) {
            for (int i = 0; i < 8; i++) {
                palette[i] = new Color((Random.Range(20, 255) / 255f), (Random.Range(20, 255) / 255f), (Random.Range(20, 255) / 255f), 255);
            }
        }

        else
            Debug.LogError("Attempted to designate invalid BlockPalette palette: \"" + paletteMode + "\".");
    }
}
