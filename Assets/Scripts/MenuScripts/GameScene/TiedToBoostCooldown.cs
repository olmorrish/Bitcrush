using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controlls whether the indicator text for boost being available 
 *  appears or not.
 */
public class TiedToBoostCooldown : MonoBehaviour {

	private Text text;
	private Boost boost;


	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
		boost = GameObject.Find("Player").GetComponent<Boost>();

	}
	
	// Update is called once per frame
	void Update () {
		if(boost.boostReady){
			text.enabled = true;
		}
		else{
			text.enabled = false;
		}
	}
}
