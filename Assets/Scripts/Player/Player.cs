﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Allows the object to be controlled via WASD and Spacebar
 * Controls and restricts player movement speed
 * This class can be modified to include fine-tuning in combo with the State class
 */
public class Player : MonoBehaviour {

    //states
    public bool onGround = true;
    public bool jumpHeldDown = false;
    public bool facingRight = true;

    //jump values
    public float jumpForce = 300f;
	public float continuousJumpForce = 5f;	   //force added by holding down the button
	public float continuousJumpDecay = 0.5f;   //amount by which the upwards force of holding "jump" decreases each frame
    private float maxContJumpForce;
    [Range(0, 1)] public float airControlMultiplier;
    public float airControlDecay = 0.001f;

    //walk values
    public float horizontalForce = 750f;
    public float maxHorizontalVelocity = 4f;
    [Range(0, 1)] public float decelerationMultiplier = 0.5f; //horizontal velocity is multiplied by this on each frame where no horiz input is given
        
    //input
    private float HorizontalIn; //the horizontal input; updates each frame

    //player component references
    private Rigidbody2D playerRB;

    //ground
    public LayerMask whatIsGround;
    public Transform groundChecker;
    const float groundCheckerRadius = 0.1f;   //radius around ground marker to collision-check
	
	// Use this for initialization
	void Awake () {

        onGround = false;
        jumpHeldDown = false;

        maxContJumpForce = continuousJumpForce;
		playerRB = GetComponent<Rigidbody2D>();
	}

    void Update() {
            
    }

    void FixedUpdate () {

        ///////////////////////////
        // Fetch Inputs
        ///////////////////////////

        HorizontalIn = Input.GetAxis("Horizontal");

        ///////////////////////////
        // Jump Reset Updates
        ///////////////////////////

        //gather all colliders the ground hit; if one is on the "Environment" layer, reset the jump

        Collider2D[] groundCollisions = Physics2D.OverlapCircleAll
            (groundChecker.position, groundCheckerRadius, whatIsGround);
        foreach(Collider2D col in groundCollisions) {
            Debug.Log("Ground collision activated.");
            if (whatIsGround.Contains(col.gameObject.layer)) {  //utilizes extension method!
                onGround = true;
            }
        }


        ///////////////////////////
        // Vertical Player Movement
        ///////////////////////////

        if (Input.GetButton("Jump")){	 
		
			//begin to jump condition
			if(onGround){
				playerRB.AddForce(new Vector3(0, 1.0f, 0) * jumpForce, ForceMode2D.Impulse);
				onGround = false;	
				jumpHeldDown = true;
			}
			
			//hold to jump higher
			else if(!onGround && jumpHeldDown){
				
				playerRB.AddForce(new Vector3(0, 1.0f, 0) * continuousJumpForce, ForceMode2D.Impulse);
				
				//update the continuous force being added by holding 
				if(continuousJumpForce<0){
					continuousJumpForce = 0;
					jumpHeldDown = false;	//disables the flag - no more effect
				}
				else if(continuousJumpForce>0){
					continuousJumpForce -= continuousJumpDecay;
				}
			}

			//otherwise the player is not on ground and jump is not held down
			else{
				jumpHeldDown = false;
			}
			
			//ensures falling players cannot jump
			if(playerRB.velocity.y < 0){
				jumpHeldDown = false;
			}
		}


        /////////////////////////////
        // Horizontal Player Movement
        /////////////////////////////

        //right input
        if (HorizontalIn > 0){
            if (!onGround)
                playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce * airControlMultiplier, ForceMode2D.Force);
            else
                playerRB.AddForce((new Vector3(1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }

        //left input
		if(HorizontalIn < 0){
            if (!onGround)
                playerRB.AddForce((new Vector3(-1, 0, 0)) * horizontalForce * airControlMultiplier, ForceMode2D.Force);
            else
                playerRB.AddForce((new Vector3(-1, 0, 0)) * horizontalForce, ForceMode2D.Force);
        }

        //slow the player if they are on the ground and giving no input
        //  this ensures landing and running don't result in slipping
        if (HorizontalIn == 0 && onGround) {
            playerRB.velocity = new Vector2(decelerationMultiplier * playerRB.velocity.x, playerRB.velocity.y);
        }

        //update or reset airControlMultiplier
        if (onGround){
			airControlMultiplier = 1;
			continuousJumpForce = maxContJumpForce;
		}
		if(!onGround && airControlMultiplier>0){
			airControlMultiplier -= airControlDecay;
		}

		// Restrict Horizontal Velocity
		if(playerRB.velocity.x > maxHorizontalVelocity){
			playerRB.AddForce((new Vector3(-1,0,0)) * horizontalForce, ForceMode2D.Force);
		}
		
		if(playerRB.velocity.x < -maxHorizontalVelocity){
			playerRB.AddForce((new Vector3(1,0,0)) * horizontalForce, ForceMode2D.Force);
		}
	}


    private void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
