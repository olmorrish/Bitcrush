using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsPlayerOnTouch : MonoBehaviour {

	public GameObject player;
	private Collider2D pCol; 
	private Collider2D myCol;
	
	// Use this for initialization
	void Awake () {
		pCol = player.GetComponent<Collider2D>();
		myCol = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(myCol.IsTouching(pCol)){
			player.GetComponent<PlayerExplodesIntoPixels>().Explode();
		}
	}
}
