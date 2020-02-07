using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLineController : MonoBehaviour {

	public GameObject gameMaster;
    private ScoreData scoreData;

	// Use this for initialization
	void Start () {
		scoreData = gameMaster.GetComponent<ScoreData>();
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreData.hiScore < 15)
            MoveUp(0.002f);

        else
            MoveUp(0.0045f);
	}
	
	void MoveUp(float increase){
		Vector3 pos = transform.position;
		pos.y += increase;
		transform.position = pos;
	}
}
