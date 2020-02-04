using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour {

	private AudioSource menuHit; 
	private AudioSource menuSelect;
	
	// Use this for initialization
	void Awake () {
		menuHit = GameObject.Find("MenuHit").GetComponent<AudioSource>();
		menuSelect = GameObject.Find("MenuSelect").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
