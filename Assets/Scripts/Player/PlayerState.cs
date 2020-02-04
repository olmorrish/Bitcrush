using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

	public bool onGround;		 
	public bool jumpHeldDown;

	// Use this for initialization
	void Awake () {
		onGround = false;
		jumpHeldDown = false;

	}

}
