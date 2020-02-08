using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplodesIntoPixels : MonoBehaviour {

	public float collisionSpeedtoExplode = 1f;
	public int numPixels= 25;
	public GameObject pixel;
	public bool exploded;
	
	private Collider2D col;
	private SpriteRenderer rend; 
	private Rigidbody2D rb;
	public AudioSource explodeFX;

    public GameObject killLine;
    private Collider2D killLineCollider;

	// Use this for initialization
	void Awake () {
		col = GetComponent<Collider2D>();
		rend = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();

        killLineCollider = killLine.GetComponent<Collider2D>();

		exploded = false;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedtoExplode){
			Explode();
		}
        else if (collision.gameObject.Equals(killLine)) {
            Explode();
        }
	}
	void OnCollisionStay2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedtoExplode){
			Explode();
		}
        else if (collision.gameObject.Equals(killLine)) {
            Explode();
        }
    }
	
	public void Explode(){
		
		if(!exploded){
			exploded = true;
            explodeFX.Play();
			
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
		}
	}
}
