using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Allows the object to be controlled via (1) Horziontal Axis input and (2) "Jump" button.  
 * Setup:
 *  After attaching the script to a player-controlled object, 
 *  (1) Create an empty object as a child of the player, and drag it to the "groundChecker" reference. Ensure it is below the player.
 *  (2) Define the LayerMask "whatIsGround" to decide what should reset a player jump when touched by the checker.
 *  (3) Set "jumpFX" as an audio clip to play on jump.
 */
public class Player : MonoBehaviour {

    //timing variables for button inputs
    private float jumpDownTrueUntil = -1f;
    public float inputMemoryLength = 0.075f;     //time until a button input is forgotten
    private float horizontalIn; //the horizontal input; updates each frame

    //state booleans
    public bool onGround = false;
    public bool jumpHeldDown = false;

    //jump values
    public float jumpForce = 15f;
	public float continuousJumpForce = 250f;	   //force added by holding down the button
	public float continuousJumpDecay = 15f;   //amount by which the upwards force of holding "jump" decreases each frame
    private float maxContJumpForce;
    [Range(0, 1)] public float airControlMultiplier;
    public float airControlDecay = 0.001f;

    public float jumpResetDeadZoneTime = 0.75f;  //the time after jumping before a jump reset is permitted
    private float jumpResetBlockedUntil;        //marks the time at which a jump reset is now okay 

    //walk values
    public float horizontalForce = 900f;
    public float maxHorizontalVelocity = 4f;
    [Range(0, 1)] public float groundDecelerationMultiplier = 0.7f;   //horizontal velocity is multiplied by this on each frame where no horiz input is given
    [Range(0, 1)] public float airDecelerationMultiplier = 0.7f;

    //player component references
    private Rigidbody2D playerRB;
    public AudioSource jumpFX;

    //jump reset variables
    public LayerMask whatIsGround;
    public Transform groundChecker;
    public float groundCheckerRadius = 0.05f;
	
	// Use this for initialization
	void Awake () {
        onGround = false;
        jumpHeldDown = false;
        jumpResetBlockedUntil = Time.time;

        maxContJumpForce = continuousJumpForce;
		playerRB = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButton("Jump")) {
            jumpDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
    }

    void FixedUpdate () {
        JumpResetCheck();
        ApplyVerticalPhysics();
        horizontalIn = Input.GetAxis("Horizontal"); //TODO, move to Update somehow
        ApplyHorizontalPhysics(horizontalIn);
	}

    private void JumpResetCheck() {

        Collider2D[] groundCollisions = Physics2D.OverlapCircleAll (groundChecker.position, groundCheckerRadius, whatIsGround);

        if (Time.time > jumpResetBlockedUntil) {
            foreach (Collider2D col in groundCollisions) {
                //Debug.Log("Ground collision detected.");
                if (whatIsGround.Contains(col.gameObject.layer)) {  //utilizes extension method!
                    onGround = true;
                    jumpResetBlockedUntil = Time.time + jumpResetDeadZoneTime; //set the point at which a jump reset may occur again
                }
            }
        }
    }

    private void ApplyVerticalPhysics() {

        if (Time.time > jumpDownTrueUntil) {  //Check if the jump button cooldown has expired. If so, reset it.
            jumpDownTrueUntil = -1f;
            jumpHeldDown = false;
        }

        //process the jump input
        else {
            //begin to jump condition
            if (onGround && !jumpHeldDown) {
                onGround = false;
                jumpHeldDown = true;
                playerRB.AddForce(new Vector3(0, 1.0f, 0) * jumpForce, ForceMode2D.Impulse);
                jumpFX.Play();
            }

            //hold to jump higher
            else if (!onGround && jumpHeldDown) {
                playerRB.AddForce(new Vector3(0, 1.0f, 0) * continuousJumpForce, ForceMode2D.Force);

                if (continuousJumpForce < 0) {
                    continuousJumpForce = 0;
                    jumpHeldDown = false;   
                }
                else if (continuousJumpForce > 0) {
                    continuousJumpForce -= continuousJumpDecay;
                }
            }

            //otherwise the player is not on ground and jump is not held down
            else {
                jumpHeldDown = false;
            }
        }
    }

    private void ApplyHorizontalPhysics(float horizontalIn) {
        
        //apply physics
        if (!onGround)
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * horizontalIn * airControlMultiplier, ForceMode2D.Force);
        else
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * horizontalIn, ForceMode2D.Force);

        //slow the player if they are giving no input
        //  makes movement "snappier"
        if(horizontalIn == 0) {
            if (onGround)
                 playerRB.velocity = new Vector2(groundDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
            else
                playerRB.velocity = new Vector2(airDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
        }

        //update or reset airControlMultiplier
        if (onGround) {
            airControlMultiplier = 1;
            continuousJumpForce = maxContJumpForce;
        }
        if (!onGround && airControlMultiplier > 0) {
            airControlMultiplier -= airControlDecay;
        }

        // Restrict Horizontal Velocity
        if (playerRB.velocity.x > maxHorizontalVelocity) {
            playerRB.AddForce((new Vector3(-1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }
        else if (playerRB.velocity.x < -maxHorizontalVelocity) {
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }
    }
}
