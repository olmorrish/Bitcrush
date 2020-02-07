using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesUpwards : MonoBehaviour {

	public GameObject gameMaster;
    private GameData gameData;

	// Use this for initialization
	void Start () {
		gameData = gameMaster.GetComponent<GameData>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameData.hiScore < 15) {
            MoveUp(0.002f);
        }

        else {
            MoveUp(0.0045f);
        }	
	}
	
	void MoveUp(float increase){
		Vector3 pos = transform.position;
		pos.y += increase;
		transform.position = pos;
	}
}
