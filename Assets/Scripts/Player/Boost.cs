using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {
	
	public int boostCooldownMax = 1200;	//20s
	public int boostCooldown;
	
	public GameObject pixel;
	public int numPixels;
	
	private Rigidbody2D playerRB;
	private PlayerState state;
	private MoveWithWASD wasd;
	
	private AudioSource boostFX;
	
	// Use this for initialization
	void Awake () {
		playerRB = GetComponent<Rigidbody2D>();
		state = GetComponent<PlayerState>();
		wasd = GetComponent<MoveWithWASD>();
		
		boostCooldown = boostCooldownMax;
		
		boostFX = GameObject.Find("BoostFX").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(boostCooldown>0){
			boostCooldown -= 1;
		}
		
		if(Input.GetButtonDown("Boost") && boostCooldown == 0){
			ApplyBoost();
			boostCooldown = boostCooldownMax;
		}
	}


	public void ApplyBoost(){
		
		boostFX.Play();

		playerRB.AddForce(new Vector3(0, 1.5f, 0) * wasd.jumpForce, ForceMode2D.Impulse);
		state.onGround = false;	
		state.jumpHeldDown = true;
		
		for(int i=0; i<numPixels; i++){
			GameObject pixelClone = (GameObject) Instantiate(pixel, transform.position, transform.rotation);
			pixelClone.GetComponent<PixelBurstBehavior>().Fling();
			pixelClone.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,-1f,0), ForceMode2D.Impulse);	//adds an impulse
		}
		

	}
}
