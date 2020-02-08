using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckerBehavior : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;

    public GameObject[] trominoObjects;
    public GameObject[] tetrominoObjects;
    public GameObject[] pentominoObjects;

    //game mode modifiers
	public bool fireTrominoes = false; 
	public bool fireTetrominoes = false; 
	public bool firePentominoes = false; 
    public bool rotateBy45Degrees = false;
	
	private GameObject toSpawn;
	
	public void Throw(){

        if (fireTetrominoes) {
            int pick = Random.Range(0, 7);
            toSpawn = tetrominoObjects[pick];
        }
        else if (fireTrominoes) {
            int pick = Random.Range(0, 2);
            toSpawn = trominoObjects[pick];
        }
        else if (firePentominoes) {
            int pick = Random.Range(0, 18);
            toSpawn = pentominoObjects[pick];
        }
		  
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
        if (rotateBy45Degrees)
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
