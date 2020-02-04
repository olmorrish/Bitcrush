using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelBurstBehavior : MonoBehaviour {

	private Rigidbody2D rb;
	public Vector3 baseTrajectory;
	public float trajectoryRandomFactor = 0.0f;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		Fling();
		
	}
	
	void Update(){
		if(rb.velocity.y<-50){
			Destroy(gameObject);
		}
	}
	
	public void Fling(){
		float xtraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		float ytraj = Random.Range(-trajectoryRandomFactor, trajectoryRandomFactor);
		Vector3 trajectory = new Vector3(baseTrajectory.x + xtraj, baseTrajectory.y + ytraj, 0);
		
		rb.AddForce(trajectory, ForceMode2D.Impulse);
		
		///
		
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
		GetComponent<SpriteRenderer>().color = colour;
	}
}
