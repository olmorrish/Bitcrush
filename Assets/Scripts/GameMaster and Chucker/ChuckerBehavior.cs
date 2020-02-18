using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChuckerBehavior : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;

    //block prefab references
    public GameObject[] tinyBlockObjects;
    public GameObject[] tetrominoObjects;
    public GameObject[] trominoObjects;
    public GameObject[] pentominoObjects;
    public GameObject octominoF;
    public GameObject[] warpedObjects;

    private GameObject toSpawn;
    private bool chuckerBlocked; //prevents the chucker from actually throwing if there is block nearby
    public float chuckerBlockedRadius;

    public void Throw(string fireMode, bool rotate45, BlockPalette blockPalette){

        if (fireMode.Equals("tetromino")) 
            toSpawn = tetrominoObjects[Random.Range(0, 7)];
        else if (fireMode.Equals("tromino")) 
            toSpawn = trominoObjects[Random.Range(0, 2)];
        else if (fireMode.Equals("pentomino"))
            toSpawn = pentominoObjects[Random.Range(0, 18)];
        else if (fireMode.Equals("tiny"))
            toSpawn = tinyBlockObjects[Random.Range(0, 2)];
        else if (fireMode.Equals("all")) {
            int set = Random.Range(0,3);
            if (set == 0) toSpawn = tetrominoObjects[Random.Range(0, 7)];
            else if (set == 1) toSpawn = trominoObjects[Random.Range(0, 2)];
            else if (set == 2) toSpawn = pentominoObjects[Random.Range(0, 18)];
         }
        else if (fireMode.Equals("warped"))
            toSpawn = warpedObjects[Random.Range(0, 17)];
        else if (fireMode.Equals("university"))
            toSpawn = octominoF;
		  
		//how fast?
		float xtraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		float ytraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
        Vector3 trajectory = new Vector3(baseTrajectory.x + xtraj, baseTrajectory.y + ytraj, 0);

        //what color?
        Color colour = blockPalette.palette[Random.Range(0, 8)]; 		

        //how much rotation?
        float rotation = 90f * (Mathf.Floor(Random.Range(0, 4)));

        // optional mode modifier
        if (rotate45)
            rotation += 45f;

        chuckerBlocked = AmIBlocked();  //check the surroundings to ensure you can fire without overlapping somethign else

        if (!chuckerBlocked) {
            GameObject tetrominoClone = (GameObject)Instantiate(toSpawn, transform.position, transform.rotation);
            tetrominoClone.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            //tetrominoClone.GetComponent<Rigidbody2D>().AddTorque(rot);
            tetrominoClone.GetComponent<SpriteRenderer>().color = colour;

            Quaternion finalRotation = Quaternion.Euler(0, 0, rotation);
            //finalRotation.z = rotation; 
            tetrominoClone.transform.rotation = finalRotation;
        }
        else
            Debug.Log(gameObject.name + " was blocked from throwing.");
	}


    private bool AmIBlocked() {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, chuckerBlockedRadius);
        if (nearby.Length > 0)
            return true;
        else
            return false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, chuckerBlockedRadius);
    }
}
