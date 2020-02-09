using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChuckerBehavior : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;

    public GameObject[] tetrominoObjects;
    public GameObject[] trominoObjects;
    public GameObject[] pentominoObjects;
    public GameObject octominoF;

    //game mode modifiers
	
	private GameObject toSpawn;

    public void Throw(string fireMode, bool rotate45){

        if (fireMode.Equals("tetromino")) 
            toSpawn = tetrominoObjects[Random.Range(0, 7)];
        else if (fireMode.Equals("tromino")) 
            toSpawn = trominoObjects[Random.Range(0, 2)];
        else if (fireMode.Equals("pentomino"))
            toSpawn = pentominoObjects[Random.Range(0, 18)];
        else if (fireMode.Equals("all")) {
            int set = Random.Range(0,3);
            if (set == 0) toSpawn = tetrominoObjects[Random.Range(0, 7)];
            else if (set == 1) toSpawn = trominoObjects[Random.Range(0, 2)];
            else if (set == 2) toSpawn = pentominoObjects[Random.Range(0, 18)];
         }
        else if (fireMode.Equals("university"))
            toSpawn = octominoF;
		  
		//how fast?
		float xtraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		float ytraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
        Vector3 trajectory = new Vector3(baseTrajectory.x + xtraj, baseTrajectory.y + ytraj, 0);

        //what color?
        Color colour = new Color(0,0,0); 		
		int tint = Random.Range(0, 8);
		switch (tint) {
			case 0: 
				colour = new Color(0,1,0); break;
			case 1: 
				colour = new Color(1,0,1); break;
			case 2: 
				colour = new Color(1,0,0); break;
			case 3: 
				colour = new Color(0,0,1); break;
			case 4: 
				colour = new Color(1,1,0); break;
			case 5: 
				colour = new Color(1,150f/255f, 0); break;
			case 6: 
				colour = new Color(0, 1,150f/255f); break;
			case 7: 
				colour = new Color(150f/255f, 0, 1); break;
			default:
				colour = new Color(0,0,0); break;
		  }

        //how much rotation?
        float rotation;
        rotation = 90f * (Mathf.Floor(Random.Range(0, 4)));

        //mode modifier
        if (rotate45)
            rotation += 45f;

		GameObject tetrominoClone = (GameObject) Instantiate(toSpawn, transform.position, transform.rotation);
		tetrominoClone.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
		//tetrominoClone.GetComponent<Rigidbody2D>().AddTorque(rot);
		tetrominoClone.GetComponent<SpriteRenderer>().color = colour;
		
		Quaternion finalRotation = Quaternion.Euler(0,0,rotation);
		//finalRotation.z = rotation; 
		tetrominoClone.transform.rotation = finalRotation;
	}
}
