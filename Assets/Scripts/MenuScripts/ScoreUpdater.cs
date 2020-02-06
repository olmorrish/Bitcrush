using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	private GameObject player;
	private float height;
	private Text scoreText;
	public float maxScore;
	
	// Use this for initialization
	void Start () {
		maxScore = 0;
		player = GameObject.Find("Player");
		scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		height = player.transform.position.y;
		if(height > maxScore){
			maxScore = height;
			scoreText.text = "SCORE: " + (Mathf.Floor(height)).ToString();
		}
		
	}
}
