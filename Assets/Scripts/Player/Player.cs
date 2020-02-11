using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* @author Oliver Morrish
 * Allows the object to be controlled via (1) Horziontal Axis input and (2) "Jump" button.  
 * Setup:
 *  After attaching the script to a player-controlled object, 
 *  (1) Create an empty object as a child of the player, and drag it to the "groundChecker" reference. Ensure it is below the player.
 *  (2) Define the LayerMask "whatIsGround" to decide what should reset a player jump when touched by the checker.
 *  (3) Set "jumpFX" as an audio clip to play on jump.
 *  (4) Tweak values.
 */
public class Player : MonoBehaviour {

    public bool slipperyJumpAllowed;    //bitcrush specific, assigned to bypass the low velocity requirement for slippery slopes mode

    //timing variables for button inputs
    private float jumpDownTrueUntil = -1f;       
    public float inputMemoryLength = 0.075f;     // time until a button input is forgotten
    private float horizontalIn;                  // the horizontal input; updates each frame

    //state booleans
    public bool onGroundCanJump = false;
    public bool jumpNotReleased = false;

    //jump values
    public float jumpForce = 15f;
	public float continuousJumpForce = 250f;	    //force added by holding down the button
	public float continuousJumpDecay = 15f;     //amount by which the upwards force of holding "jump" decreases each frame
    private float maxContJumpForce;
    [HideInInspector] [Range(0, 1)] public float airControlMultiplier;
    public float airControlDecay = 0.001f;

    public float jumpResetDeadZoneTime = 0.75f;  //the time after jumping before a jump reset is permitted
    public float jumpResetBlockedUntil;         //marks the time at which a jump reset is now okay 

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

    private Animator playerAnim;
	
	// Use this for initialization
	void Awake () {
        onGroundCanJump = false;
        jumpNotReleased = false;
        jumpResetBlockedUntil = Time.time;

        maxContJumpForce = continuousJumpForce;
		playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetButton("Jump")) {
            jumpDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
        else
            jumpNotReleased = false;

        horizontalIn = Input.GetAxis("Horizontal");
    }

    void FixedUpdate () {
        //jump reset check is only permitted if:
        //  (a) reset cooldown is done (b) reset is not already active (c) player is veritcally stationary
        if ((Time.time > jumpResetBlockedUntil) && !onGroundCanJump && 
            (   (!(Mathf.Abs(playerRB.velocity.y) > 0.1f)) || slipperyJumpAllowed)  )
            JumpResetCheck();

        ApplyVerticalPhysics();
        
        ApplyHorizontalPhysics(horizontalIn);
	}

    private void JumpResetCheck() {

        Collider2D[] groundCollisions = Physics2D.OverlapCircleAll (groundChecker.position, groundCheckerRadius, whatIsGround);

        foreach (Collider2D col in groundCollisions) {
            //Debug.Log("Ground collision detected.");
            if (whatIsGround.Contains(col.gameObject.layer)) {  //utilizes extension method!
                onGroundCanJump = true;
                continuousJumpForce = maxContJumpForce;
                Debug.Log("Jump has been reset.");
                jumpResetBlockedUntil = Time.time + jumpResetDeadZoneTime; //set the point at which a jump reset may occur again
            }
        }
    }

    private void ApplyVerticalPhysics() {

        if (Time.time > jumpDownTrueUntil) {  //Check if the jump button cooldown has expired. If so, reset it.
            jumpDownTrueUntil = -1f;
            jumpNotReleased = false;
            playerAnim.SetBool("isBeginningJump", false); //bitcrush-specific
        }

        //process the jump input
        else {
            //begin to jump condition
            if (onGroundCanJump /*&& !jumpHeldDown*/) {
                Debug.Log("Beginning a new jump.");
                onGroundCanJump = false;
                jumpNotReleased = true;
                playerRB.AddForce(new Vector3(0, 1.0f, 0) * jumpForce, ForceMode2D.Impulse);
                jumpResetBlockedUntil = Time.time + jumpResetDeadZoneTime;      //stop a jump reset for a while after jumping

                playerAnim.SetBool("isBeginningJump", true); //bitcrush-specific
                jumpFX.Play();
            }

            //hold to jump higher
            else if (!onGroundCanJump && jumpNotReleased) {
                playerRB.AddForce(new Vector3(0, 1.0f, 0) * continuousJumpForce, ForceMode2D.Force);
                playerAnim.SetBool("isBeginningJump", false); //bitcrush-specific

                if (continuousJumpForce <= 0) {
                    continuousJumpForce = 0;  
                }
                else if (continuousJumpForce > 0) {
                    continuousJumpForce -= continuousJumpDecay;
                }
            }

            //otherwise the player is not on ground and jump is not held down
            else {
                playerAnim.SetBool("isBeginningJump", false); //bitcrush-specific
            }
        }
    }

    private void ApplyHorizontalPhysics(float horizontalIn) {
        
        //apply physics
        if (!onGroundCanJump)
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * horizontalIn * airControlMultiplier, ForceMode2D.Force);
        else
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * horizontalIn, ForceMode2D.Force);

        //slow the player if they are giving no input
        //  makes movement "snappier"
        if(horizontalIn == 0) {
            if (onGroundCanJump)
                 playerRB.velocity = new Vector2(groundDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
            else
                playerRB.velocity = new Vector2(airDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
        }

        //update or reset airControlMultiplier
        if (onGroundCanJump) {
            airControlMultiplier = 1;
            continuousJumpForce = maxContJumpForce;
        }
        if (!onGroundCanJump && airControlMultiplier > 0) {
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

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundChecker.position, groundCheckerRadius);
    }
}
