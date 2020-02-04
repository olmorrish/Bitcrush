using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesUpwards : MonoBehaviour {

	private GameObject score;
	private ScoreUpdater scoreUpdater;

	// Use this for initialization
	void Start () {
		score = GameObject.Find("ScoreText");
		scoreUpdater = score.GetComponent<ScoreUpdater>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(scoreUpdater.maxScore < 15){
			MoveUp(0.002f);
		}
		//else if(scoreUpdater.maxScore < 30){
		//	
		//}
		else{
			MoveUp(0.0045f);
		}
		
		
		
		
		
		
	}
	
	
	
	
	
	
	void MoveUp(float increase){
		Vector3 pos = transform.position;
		pos.y += increase;
		transform.position = pos;
	}
}
