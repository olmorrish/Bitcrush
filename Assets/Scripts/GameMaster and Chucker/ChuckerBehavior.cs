using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckerBehavior : MonoBehaviour {

	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;
	
	public GameObject trominoLine;
	public GameObject trominoTurn;
	public GameObject red;
	public GameObject orange;
	public GameObject yellow;
	public GameObject green;
	public GameObject blue;
	public GameObject indigo;
	public GameObject violet;
	
	public GameObject pentoF;
	public GameObject pentoF2;
	public GameObject pentoI;
	public GameObject pentoL;
	public GameObject pentoL2;
	public GameObject pentoN;
	public GameObject pentoN2;
	public GameObject pentoP;
	public GameObject pentoP2;
	public GameObject pentoT;
	public GameObject pentoU;
	public GameObject pentoV;
	public GameObject pentoW;
	public GameObject pentoX;
	public GameObject pentoY;
	public GameObject pentoY2;
	public GameObject pentoZ;
	public GameObject pentoZ2;
	
	public bool fireOnlyTrominoes = false; 
	public bool fireOnlyTetrominoes = false; 
	public bool fireOnlyPentominoes = false; 
	
	
	private GameObject obj;
	
	public bool fire = false;
	public int startFiringOffset = 200;

	
	// Update is called once per frame
	void Update () {

	}
	
	
	
	
	
	
	
	public void Throw(){
		
		if(fireOnlyTetrominoes){
			int pick = Random.Range(0, 7);
			switch (pick)
			{
				case 0:
					obj = red;
					break;
				case 1:
					obj = orange;
					break;
				case 2:
					obj = yellow;
					break;
				case 3:
					obj = green;
					break;
				case 4:
					obj = blue;
					break;
				case 5:
					obj = indigo;
					break;
				case 6:
					obj = violet;
					break;
				default:
					break;
			}	
		}
		
		else if(fireOnlyTrominoes){
			int pick = Random.Range(0, 2);
			switch (pick)
			{
				case 0:
					obj = trominoLine;
					break;
				case 1:
					obj = trominoTurn;
					break;
				default:
					break;
			}	
		}
		
		
		else if(fireOnlyPentominoes){
			int pick = Random.Range(0, 18);
			switch (pick)
			{
				case 0:
					obj = pentoF; break;
				case 1:
					obj = pentoF2; break;
				case 2:
					obj = pentoI; break;
				case 3:
					obj = pentoL; break;
				case 4:
					obj = pentoL2; break;
				case 5:
					obj = pentoN; break;
				case 6:
					obj = pentoN2; break;
				case 7:
					obj = pentoP; break;
				case 8:
					obj = pentoP2; break;
				case 9:
					obj = pentoT; break;
				case 10:
					obj = pentoU; break;
				case 11:
					obj = pentoV; break;
				case 12:
					obj = pentoW; break;
				case 13:
					obj = pentoX; break;
				case 14:
					obj = pentoY; break;
				case 15:
					obj = pentoY2; break;
				case 16:
					obj = pentoZ; break;
				case 17:
					obj = pentoZ2; break;
				default:
					break;
			}	
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
