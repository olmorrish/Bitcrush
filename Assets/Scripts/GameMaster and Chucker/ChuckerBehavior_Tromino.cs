using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckerBehavior_Tromino : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;
	
	public GameObject tromino_turn;
	public GameObject tromino_line;
	
	private GameObject obj;
	
	public bool fire = false;
	public int startFiringOffset = 200;

	
	// Update is called once per frame
	void Update () {
		
		/*
		if(fire){
			Throw();
			fire = false;
		}
		if(startFiringOffset >0){
			startFiringOffset -= 1;
		}
		else{
			fire = true;
			startFiringOffset = 400;
		}
		
		*/
	}
	
	
	
	
	
	
	
	public void Throw(){
		
		//which block?
		int pick = Random.Range(0, 2);
		switch (pick)
		{
			case 0:
				obj = tromino_turn;
				break;
			case 1:
				obj = tromino_line;
				break;
			default:
				break;
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
