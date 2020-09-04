using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockNextButtonHandler : MonoBehaviour {

    public GameObject menuNavigatorObj;
    private MainMenu mainMenu;
    public GameObject unlockTextObj;
    private Text unlockText;
    public List<string> unlockMessageQueue;

    // Start is called before the first frame update
    void Start() {
        mainMenu = menuNavigatorObj.GetComponent<MainMenu>();
        unlockText = unlockTextObj.GetComponent<Text>();

        //grab any unlock messages from the settings
        PersistentSettings persistentSettings = GameObject.Find("PersistentSettingsObject").GetComponent<PersistentSettings>();
        this.unlockMessageQueue = persistentSettings.unlockMessageQueue;
        persistentSettings.unlockMessageQueue = new List<string>();         //wipe the unlock list once we have it
        if(unlockMessageQueue.Count > 0) {
            ShowNextUnlock(); //subsequent calls to ShowNextUnlock are handled by user input
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    /* Show Next Unlock
     * Either closes the unlock screen or updates the text, based on what is queued.
     * Called by on scene load if a message is available, then called again when button is hit.
     * Actual unlocks for PlayerPrefs are handled by the GameOverMenu script when a game is completed.
     */ 
    void ShowNextUnlock() {
        if(unlockMessageQueue.Count < 1) {
            mainMenu.SwitchToCanvas("MainMenu");
        }
        else {
            unlockText.text = unlockMessageQueue[0];
            unlockMessageQueue.RemoveAt(0);
        }
    }
}
