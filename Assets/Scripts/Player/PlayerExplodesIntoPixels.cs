using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplodesIntoPixels : MonoBehaviour {

	public float collisionSpeedToExplode = 1f;
	public int numPixels= 25;
	public GameObject pixel;
	public bool exploded;
	
	private Collider2D playerCollider;
	private SpriteRenderer playerRenderer; 
	private Rigidbody2D playerRB;
	public AudioSource explodeFX;

    public GameObject killLine;
    private Collider2D killLineCollider;

	// Use this for initialization
	void Awake () {
		playerCollider = GetComponent<Collider2D>();
		playerRenderer = GetComponent<SpriteRenderer>();
		playerRB = GetComponent<Rigidbody2D>();

        killLineCollider = killLine.GetComponent<Collider2D>();

		exploded = false;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedToExplode){
			Explode();
		}
        else if (collision.gameObject.Equals(killLine)) {
            Explode();
        }
	}
	void OnCollisionStay2D(Collision2D collision){
		if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedToExplode){
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
				pixelClone.GetComponent<Rigidbody2D>().AddForce(playerRB.velocity * -0.2f, ForceMode2D.Impulse);	//adds an impulse based on player velocity at time of death
			}
			
			playerCollider.enabled = false;
			playerRenderer.enabled = false;
			playerRB.velocity = Vector3.zero;
			playerRB.gravityScale = 0f;
			playerRB.mass = 999999999;
		}
	}
}
