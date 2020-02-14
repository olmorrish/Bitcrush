using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour {

    public GameObject gameMaster;          
    private GamePattern gameMasterPattern;  

    public bool boostEnabled = true; //disabled by pause and gameover menus

    //behaviour values
    public float boostVelocity = 15f;
    public float cooldownLength = 10.0f;
    private float cooldownDoneTime;
    public int numPixels;

    //timing variables for button inputs
    private float boostDownTrueUntil = -1f;
    public float inputMemoryLength = 0.075f; //time until a button input is forgotten

    //state variables
    [HideInInspector] public bool boostReady = false;  //flag whether player can boost or not - accessed by text prompt

    //gameObject and component references
    public GameObject pixel;
	private Rigidbody2D playerRB;
	private Player player;
	public AudioSource boostFX;
	
	// Use this for initialization
	void Awake () {
        boostEnabled = true;

		playerRB = GetComponent<Rigidbody2D>();
		player = GetComponent<Player>();
		
		cooldownDoneTime = Time.time;    //player immediately has boost
        boostReady = false;

        gameMasterPattern = gameMaster.GetComponent<GamePattern>();
    }
	
	// Update is called once per frame
	void Update () {

        if (cooldownDoneTime > Time.time) {   //we haven't reached the cooldown time yet
            boostReady = false;
        }
        else {
            boostReady = true;
        }

        if (Input.GetButtonDown("Boost")) {
            boostDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
	}

    private void FixedUpdate() {
        if (Time.time > boostDownTrueUntil)  //Check if the boost button cooldown has expired. If so, reset it.
            boostDownTrueUntil = -1f;
        else if (boostReady && boostEnabled) {
            ApplyBoost();
            gameMasterPattern.SpawnPixels(playerRB.position, Vector2.down, 2f, 15);
        }
    }

    private void ApplyBoost(){
        //boost activation
        cooldownDoneTime = Time.time + cooldownLength;    //reset the cooldown
        boostFX.Play();

        //just setting the velocity feels much better for falling players                         
        playerRB.velocity = (new Vector3(playerRB.velocity.x, boostVelocity ,playerRB.velocity.x));

        player.onGroundCanJump = false;
        player.jumpNotReleased = true;     
	}
}
