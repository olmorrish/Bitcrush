using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stength of boost relies on player jump height - impluse
 */
public class Boost : MonoBehaviour {

    //timing variables for button inputs
    float boostDownTrueUntil = -1f;
    float inputMemoryLength = 0.5f; //time until a button input is forgotten

	public float cooldownLength = 20.0f;
    private float boostReadyPoint;

    public bool boostReady = false;  //signals UI scipts 

    public GameObject pixel;
	public int numPixels;
	
	private Rigidbody2D playerRB;
	private Player player;
	
	private AudioSource boostFX;
	
	// Use this for initialization
	void Awake () {
		playerRB = GetComponent<Rigidbody2D>();
		player = GetComponent<Player>();
		
		boostReadyPoint = Time.time + cooldownLength;    //set the point where the cooldown will be done
        boostReady = false;

        boostFX = GameObject.Find("BoostFX").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (boostReadyPoint > Time.time)
            boostReady = true;
        else
            boostReady = false;

        if (Input.GetButtonDown("Jump")) {
            boostDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
	}

    private void FixedUpdate() {
        ApplyAnyBoost();
    }

    public void ApplyAnyBoost(){

        if (Time.time > boostDownTrueUntil)  //Check if the jump button cooldown has expired. If so, reset it.
            boostDownTrueUntil = -1f;
        
        //boost activation
        else {
            boostReadyPoint = Time.time + cooldownLength;    //reset the cooldown
            boostFX.Play();

            playerRB.AddForce(new Vector3(0, 1.5f, 0) * player.jumpForce, ForceMode2D.Impulse);
            player.onGround = false;
            player.jumpHeldDown = true;

            for (int i = 0; i < numPixels; i++) {
                GameObject pixelClone = (GameObject)Instantiate(pixel, transform.position, transform.rotation);
                pixelClone.GetComponent<PixelBurstBehavior>().Fling();
                pixelClone.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -1f, 0), ForceMode2D.Impulse);   //adds an impulse
            }
        }
	}
}
