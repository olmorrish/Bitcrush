using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplodesIntoPixels : MonoBehaviour {

	public float pressureToExplode = 3;
	public int numPixels= 25;
	public GameObject pixel;
	public bool exploded;
	
	private Collider2D col;
	private SpriteRenderer rend; 
	private Rigidbody2D rb;
	private AudioSource explFX;
	
	private GameObject feet;
	private Collider2D ft_col;

	// Use this for initialization
	void Awake () {
		col = GetComponent<Collider2D>();
		rend = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		explFX = GameObject.Find("DeathFX").GetComponent<AudioSource>();
		
		exploded = false;
		
		feet = GameObject.Find("PlayerFeet");
		ft_col = feet.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	
	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > pressureToExplode){
			Explode();
		}
	}
	void OnCollisionStay2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > pressureToExplode){
			Explode();
		}
	}
	
	
	
	
	public void Explode(){
		
		if(!exploded){
			exploded = true;
			explFX.Play();
			
			for(int i = 0; i<numPixels; i++){
				GameObject pixelClone = (GameObject) Instantiate(pixel, transform.position, transform.rotation);
				pixelClone.GetComponent<PixelBurstBehavior>().Fling();
				pixelClone.GetComponent<Rigidbody2D>().AddForce(rb.velocity * -0.2f, ForceMode2D.Impulse);	//adds an impulse based on player velocity at time of death
			}
			
			col.enabled = false;
			rend.enabled = false;
			rb.velocity = Vector3.zero;
			rb.gravityScale = 0f;
			rb.mass = 999999999;
			
			ft_col.enabled = false;
		}
	}
}
