using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalettePreview : MonoBehaviour {

    public GameObject[] previewBlocks;

    // Start is called before the first frame update
    void Start() {
        SetPreviewPalette("default");
    }

    public void SetPreviewPalette(string toSet) {   //called on buttonpress calls from methods in MainMenu.cs; updates the preview
        BlockPalette blockPalette = new BlockPalette(toSet);
        for (int i = 0; i < 8; i++)
            previewBlocks[i].GetComponent<SpriteRenderer>().color = blockPalette.palette[i];
    }
}
