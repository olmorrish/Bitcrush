﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    //timing variables for button inputs
    float boostDownTrueUntil = -1f;
    float inputMemoryLength = 0.5f; //time until a button input is forgotten

	public float cooldownLength = 5.0f;
    private float cooldownDoneTime;

    public bool boostReady = false;  //flag whether player can boost or not

    public GameObject pixel;
	public int numPixels;
	
	private Rigidbody2D playerRB;
	private Player player;
	
	private AudioSource boostFX;
	
	// Use this for initialization
	void Awake () {
		playerRB = GetComponent<Rigidbody2D>();
		player = GetComponent<Player>();
		
		cooldownDoneTime = Time.time + cooldownLength;    //set the point where the cooldown will be done
        boostReady = false;

        boostFX = GameObject.Find("BoostFX").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (cooldownDoneTime > Time.time) {   //we haven't reached the cooldown time yet
            boostReady = false;
        }
        else {
            boostReady = true;
            Debug.Log("Boost is ready.");
        }

        if (Input.GetButtonDown("Boost")) {
            boostDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
	}

    private void FixedUpdate() {
        if (Time.time > boostDownTrueUntil)  //Check if the boost button cooldown has expired. If so, reset it.
            boostDownTrueUntil = -1f;
        else if (boostReady) {
            ApplyBoost();
            SpawnBoostConfetti();
        }
    }

    private void ApplyBoost(){
        //boost activation
            cooldownDoneTime = Time.time + cooldownLength;    //reset the cooldown
            boostFX.Play();

            playerRB.AddForce(new Vector3(0, 1.5f, 0) * player.jumpForce, ForceMode2D.Impulse);
            player.onGround = false;
            player.jumpHeldDown = true;     
	}

    private void SpawnBoostConfetti() {
        for (int i = 0; i < numPixels; i++) {
            GameObject pixelClone = (GameObject)Instantiate(pixel, transform.position, transform.rotation);
            pixelClone.GetComponent<PixelBurstBehavior>().Fling();
            pixelClone.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -1f, 0), ForceMode2D.Impulse);   //adds an impulse
        }
    }
}
