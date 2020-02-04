using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionDestroysPlayer : MonoBehaviour {

	private PlayerExplodesIntoPixels exploder;
	public int ticker;

	// Use this for initialization
	void Start () {
		exploder = GameObject.Find("Player").GetComponent<PlayerExplodesIntoPixels>();
		ticker = 0;
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(!exploder.exploded){
			/*
			Debug.Log("Player heart hit!");
			exploder.Explode();
			*/
		}
		
	}
	void OnTriggerStay2D(Collider2D col){
		
		if(ticker < 3){
			ticker += 1;
		}
		
		else if(!exploder.exploded && ticker == 3){
			Debug.Log("Player heart is colliding!");
			exploder.Explode();
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		ticker = 0;
	}
	
}
