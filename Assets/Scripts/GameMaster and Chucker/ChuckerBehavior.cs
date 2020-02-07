using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckerBehavior : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;

    public GameObject[] trominoObjects;
    public GameObject[] tetrominoObjects;
    public GameObject[] pentominoObjects;
	
	public bool fireTrominoes = false; 
	public bool fireTetrominoes = false; 
	public bool firePentominoes = false; 
	
	private GameObject obj;
	
	public bool fire = false;
	public int startFiringOffset = 200;
	
	public void Throw(){

        if (fireTetrominoes) {
            int pick = Random.Range(0, 7);
            obj = tetrominoObjects[pick];
        }
        else if (fireTrominoes) {
            int pick = Random.Range(0, 2);
            obj = trominoObjects[pick];
        }
        else if (firePentominoes) {
            int pick = Random.Range(0, 18);
            obj = pentominoObjects[pick];
        }
		  
		//how fast?
		float xtraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		float ytraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		
		//how much rotation?
		float rotation = 0;
		int rotSelector = Random.Range(0,4);
		switch (rotSelector)
		{
			case 0: 
				rotation = 90f; break;
			case 1: 
				rotation = 180f; break;
			case 2: 
				rotation = 270f; break;
			default:
				rotation = 0f; break;
		  }	
		  
		//what color?
		Color colour = new Color(0,0,0); 		
		int tint = Random.Range(0, 8);
		switch (tint)
		{
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
		
		
		
		
		
		
		
		Vector3 trajectory = new Vector3(baseTrajectory.x + xtraj, baseTrajectory.y + ytraj, 0);

		GameObject tetrominoClone = (GameObject) Instantiate(obj, transform.position, transform.rotation);
		tetrominoClone.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
		//tetrominoClone.GetComponent<Rigidbody2D>().AddTorque(rot);
		tetrominoClone.GetComponent<SpriteRenderer>().color = colour;
		
		Quaternion finalRotation = Quaternion.Euler(0,0,rotation);
		//finalRotation.z = rotation; 
		tetrominoClone.transform.rotation = finalRotation;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
