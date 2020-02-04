using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollisionResetJump : MonoBehaviour {

	public GameObject playerObjReference;
	
	private PlayerState state;
	
	void Awake(){
		state = playerObjReference.GetComponent<PlayerState>();
	}
	
	
	void OnCollisionEnter2D(Collision2D col){
		state.onGround = true;
		state.jumpHeldDown = false;
	}
	

	
}
