using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour {

    public List<GameObject> menuSFXObjects;
    private List<AudioSource> menuSFX;

    public bool sfxMuted;

    private void Awake() {
        sfxMuted = false;
    }

    // Start is called before the first frame update
    void Start() {
        menuSFX = new List<AudioSource>();

        foreach(GameObject obj in menuSFXObjects) {
            menuSFX.Add(obj.GetComponent<AudioSource>());
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    /* Play SFX
     * Plays the sound effect associated with the given number.
     */
    public void PlaySFX(int soundID) {
        if(menuSFX.Count < soundID) {
            menuSFX[soundID].Play();
        }
    }

    /* Toggle Mute
     * Mutes or unmutes all sound effects.
     */
    public void ToggleMute() {

        sfxMuted = !sfxMuted;

        foreach (AudioSource fx in menuSFX) {
            fx.mute = !fx.mute;
        }
    }
}
