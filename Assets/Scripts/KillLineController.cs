using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controller for Kill Line - To be used by GameMaster
 */
public class KillLineController : MonoBehaviour {

    public float baseRaiseSpeed = 0.002f;


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

	}

    public void MoveUp(float multiplier){
		Vector3 pos = transform.position;
		pos.y += baseRaiseSpeed * multiplier;
		transform.position = pos;
	}
}
