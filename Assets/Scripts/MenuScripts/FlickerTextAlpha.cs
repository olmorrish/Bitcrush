using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickerTextAlpha : MonoBehaviour {

	private Text text; 

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Color alpha = text.color;
		
		if (alpha.a>0.7f){
			alpha.a -= 0.0125f;
		}
		else{
			alpha.a = 1f;
		}
		
		text.color = alpha;
		
	}
}
