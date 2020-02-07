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
    //public bool facingRight = true;

    //jump values
    public float jumpForce = 15f;
	public float continuousJumpForce = 250f;	   //force added by holding down the button
	public float continuousJumpDecay = 15f;   //amount by which the upwards force of holding "jump" decreases each frame
    private float maxContJumpForce;
    [Range(0, 1)] public float airControlMultiplier;
    public float airControlDecay = 0.001f;

    //walk values
    public float horizontalForce = 900f;
    public float maxHorizontalVelocity = 4f;
    [Range(0, 1)] public float decelerationMultiplier = 0.7f;   //horizontal velocity is multiplied by this on each frame where no horiz input is given
        
    //player component references
    private Rigidbody2D playerRB;
    public AudioSource jumpFX;
    private PlayerExplodesIntoPixels exploderScript;    //TODO - extract; specific to BiTCRUSH

    //jump reset variables
    public LayerMask whatIsGround;
    public Transform groundChecker;
    const float groundCheckerRadius = 0.1f;   //radius around ground marker to collision-check
	
	// Use this for initialization
	void Awake () {
        onGround = false;
        jumpHeldDown = false;

        maxContJumpForce = continuousJumpForce;
		playerRB = GetComponent<Rigidbody2D>();
        exploderScript = GetComponent<PlayerExplodesIntoPixels>();
    }

    void Update() {
        if (Input.GetButton("Jump")) {
            jumpDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.05f) {
            exploderScript.Explode();
        }
    }

    void FixedUpdate () {
        JumpResetCheck();
        ApplyVerticalPhysics();
        horizontalIn = Input.GetAxis("Horizontal"); //TODO, move to Update somehow
        ApplyHorizontalPhysics(horizontalIn);
	}

    private void JumpResetCheck() {

        Collider2D[] groundCollisions = Physics2D.OverlapCircleAll
            (groundChecker.position, groundCheckerRadius, whatIsGround);

        foreach (Collider2D col in groundCollisions) {
            //Debug.Log("Ground collision detected.");
            if (whatIsGround.Contains(col.gameObject.layer)) {  //utilizes extension method!
                onGround = true;
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
            if (onGround) {
                playerRB.AddForce(new Vector3(0, 1.0f, 0) * jumpForce, ForceMode2D.Impulse);
                onGround = false;
                jumpHeldDown = true;
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

            //ensures falling players cannot jump
            if (playerRB.velocity.y < 0) {
                jumpHeldDown = false;
            }
        }
    }

    private void ApplyHorizontalPhysics(float direction) {
        //right input
        if (direction > 0) {
            if (!onGround)
                playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * airControlMultiplier, ForceMode2D.Force);
            else
                playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }

        //left input
        if (direction < 0) {
            if (!onGround)
                playerRB.AddForce((new Vector3(-1, 0, 0)) * horizontalForce * airControlMultiplier, ForceMode2D.Force);
            else
                playerRB.AddForce((new Vector3(-1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }

        //slow the player if they are on the ground and giving no input
        //  this ensures landing and running don't result in slipping - makes movement "snappier"
        if (direction == 0 && onGround) {
            playerRB.velocity = new Vector2(decelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
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

        if (playerRB.velocity.x < -maxHorizontalVelocity) {
            playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }
    }

    //private void Flip() {
    //    // Switch the way the player is labelled as facing.
    //    facingRight = !facingRight;

    //    // Multiply the player's x local scale by -1.
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}
}
