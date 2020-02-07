using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLineController : MonoBehaviour {

	public GameObject gameMaster;
    private ScoreData scoreData;

    public GameObject player;
    private Collider2D playerCollider;
    private PlayerExplodesIntoPixels exploder;
    private Collider2D myCol;

    // Use this for initialization
    void Start () {
		scoreData = gameMaster.GetComponent<ScoreData>();
        playerCollider = player.GetComponent<Collider2D>();
        exploder = player.GetComponent<PlayerExplodesIntoPixels>();
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
