using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesTopScore : MonoBehaviour {

	private GameObject score;
	private ScoreUpdater scoreUpdater;
	
	private Vector3 newPos;

	// Use this for initialization
	void Start () {
		score = GameObject.Find("ScoreText");
		scoreUpdater = score.GetComponent<ScoreUpdater>();
	}
	
	// Update is called once per frame
	void Update () {
		newPos = transform.position;
		newPos.y = Mathf.Floor(scoreUpdater.maxScore) - 0.5f;
		transform.position = newPos;
	}
}
