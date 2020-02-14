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

    public GameObject gameMaster;          //bitcrush-specific
    private GamePattern gameMasterPattern;  //bitcrush-specific

    //timing variables for button inputs
    private float jumpDownTrueUntil = -1f;       
    private const float inputMemoryLength = 0.075f;     // time until a button input is forgotten

    //state booleans
    [HideInInspector] public bool onGroundCanJump = false;
    [HideInInspector] public bool jumpNotReleased = false;
    [HideInInspector] public bool slipperyJumpAllowed;    //bitcrush-specific, assigned to bypass the low velocity requirement for slippery slopes mode

    //jump values
    public float jumpForce = 55f;
	public float continuousJumpForce = 360f;	    //force added by holding down the button
	public float continuousJumpDecay = 25f;     //amount by which continuous force decreases each frame that jump is held
    private float maxContJumpForce;             //saves the initial value of the jumpForce so we can reset it
    public float airControlDecay = 0.001f;
    [HideInInspector] [Range(0, 1)] public float airControlMultiplier;

    public float jumpResetDeadZoneTime = 0.75f;  //the time after jumping before a jump reset is permitted
    private float jumpResetBlockedUntil;          //marks the time at which a jump reset is now okay 

    //fastfall mechanic variables
    private float verticalIn;
    public bool fastFallEnabled = false;         //TODO allows player to increase fall-speed with "down" input
    public float fastFallForce;

    //horizontal movement variables
    private float horizontalIn;                         // the horizontal input; updates each frame
    public float horizontalForce = 900f;
    public float maxHorizontalVelocity = 4f;
    [Range(0, 1)] public float groundDecelerationMultiplier = 0.7f;   //horizontal velocity is multiplied by this on each frame where no horiz input is given
    [Range(0, 1)] public float airDecelerationMultiplier = 0.7f;

    //player component references
    private Rigidbody2D playerRB;
    public AudioSource jumpFX;
    private Animator playerAnim;                 //bitcrush-specific

    //jump reset variables --> (JumpResetCheck())
    public LayerMask whatIsGround;
    public Transform groundChecker;
    public float groundCheckerRadius = 0.05f;

	// Use this for initialization
	void Awake () {
        onGroundCanJump = false;
        jumpNotReleased = false;
        jumpResetBlockedUntil = Time.time;

        maxContJumpForce = continuousJumpForce;
		playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();

        gameMasterPattern = gameMaster.GetComponent<GamePattern>();
    }

    void Update() {
        if (Input.GetButton("Jump"))
            jumpDownTrueUntil = Time.time + inputMemoryLength;   //set the time until we forget the player pressed the button
        else
            jumpNotReleased = false;

        horizontalIn = Input.GetAxis("Horizontal");
        verticalIn = Input.GetAxis("Vertical");
    }

    void FixedUpdate () {
        if ((Time.time > jumpResetBlockedUntil) && !onGroundCanJump &&                  //jump reset check is only permitted if:
            ((!(Mathf.Abs(playerRB.velocity.y) > 0.1f)) || slipperyJumpAllowed))        //  (a) reset cooldown is done (b) reset is not already active (c) player is veritcally stationary
            JumpResetCheck();

        ApplyJumpPhysics();
        ApplyVerticalPhysics(verticalIn);
        ApplyHorizontalPhysics(horizontalIn);
	}

    ///////////////////////
    /// Support Methods ///
    ///////////////////////

    private void JumpResetCheck() {
        Collider2D[] groundCollisions = Physics2D.OverlapCircleAll (groundChecker.position, groundCheckerRadius, whatIsGround);
        if (groundCollisions.Length > 0) {
            onGroundCanJump = true;
            continuousJumpForce = maxContJumpForce;
            jumpResetBlockedUntil = Time.time + jumpResetDeadZoneTime; //set the point at which a jump reset may occur again
        }
    }

    private void ApplyJumpPhysics() {

        if (Time.time > jumpDownTrueUntil) {  //Check if the jump button cooldown has expired. If so, reset it.
            jumpDownTrueUntil = -1f;
            jumpNotReleased = false;
            playerAnim.SetBool("isBeginningJump", false); //bitcrush-specific
        }

        //process the jump input
        else {
            //begin to jump condition
            if (onGroundCanJump /*&& !jumpHeldDown*/) {
                onGroundCanJump = false;
                jumpNotReleased = true;
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpResetBlockedUntil = Time.time + jumpResetDeadZoneTime;      //stop a jump reset for a while after jumping

                playerAnim.SetBool("isBeginningJump", true); //bitcrush-specific
                jumpFX.Play();
            }

            //hold to jump higher
            else if (!onGroundCanJump && jumpNotReleased) {
                playerRB.AddForce(Vector2.up * continuousJumpForce, ForceMode2D.Force);
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

    private void ApplyVerticalPhysics(float verticalIn) {
        //to fastfall, player must be in the air and not holding "Jump"
        if (fastFallEnabled && (verticalIn < 0) && !onGroundCanJump && jumpDownTrueUntil < Time.time) {
            playerRB.AddForce(Vector2.down * fastFallForce * (-verticalIn), ForceMode2D.Force);
            gameMasterPattern.SpawnPixels(playerRB.position, Vector2.up, 2f, 2);
        }
    }

    private void ApplyHorizontalPhysics(float horizontalIn) {
        
        //apply physics
        if (!onGroundCanJump)
            playerRB.AddForce(Vector2.right * horizontalForce * horizontalIn * airControlMultiplier, ForceMode2D.Force);
        else
            playerRB.AddForce(Vector2.right * horizontalForce * horizontalIn, ForceMode2D.Force);

        //slow the player if they are giving no input
        //  makes movement "snappier"
        if(horizontalIn == 0) {
            if (onGroundCanJump)
                 playerRB.velocity = new Vector2(groundDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
            else
                playerRB.velocity = new Vector2(airDecelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
        }

        //airControlMultiplier - reset or update
        if (onGroundCanJump) {
            airControlMultiplier = 1;
            continuousJumpForce = maxContJumpForce;
        }
        if (!onGroundCanJump && airControlMultiplier > 0) {
            airControlMultiplier -= airControlDecay;
        }

        // Restrict Horizontal Velocity
        if (playerRB.velocity.x > maxHorizontalVelocity) {
            playerRB.AddForce(Vector2.left * horizontalForce, ForceMode2D.Force);
        }
        else if (playerRB.velocity.x < -maxHorizontalVelocity) {
            playerRB.AddForce(Vector2.right * horizontalForce, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(groundChecker.position, groundCheckerRadius);
    }

}
