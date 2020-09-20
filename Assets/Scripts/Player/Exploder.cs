using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {

	public float collisionSpeedToExplode = 1f;
	public GameObject pixel;
    public bool immuneToCrush;
    public bool exploded;
	
	private Collider2D playerCollider;
	private SpriteRenderer playerRenderer; 
	private Rigidbody2D playerRB;
	public AudioSource explodeFX;

    public GameObject killLine;
    //private Collider2D killLineCollider;

    public GameObject gameMaster;
    private GamePattern gameMasterPattern;

    // Use this for initialization
    void Awake () {
		playerCollider = GetComponent<Collider2D>();
		playerRenderer = GetComponent<SpriteRenderer>();
		playerRB = GetComponent<Rigidbody2D>();

        //killLineCollider = killLine.GetComponent<Collider2D>(); // killLine has multiple colliders; don't use this.

		exploded = false;

        gameMasterPattern = gameMaster.GetComponent<GamePattern>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (!immuneToCrush) {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedToExplode) {
                Explode();
            }
            else if (collision.gameObject.Equals(killLine)) {
                Explode();
            }
        }
		
	}
	void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > collisionSpeedToExplode) {
            if (!immuneToCrush) {
                Explode();
            }
        }
        else if (collision.gameObject.Equals(killLine)) {
            Explode();
        }
    }
	
	public void Explode(){
		
		if(!exploded){
			exploded = true;
            explodeFX.Play();


            gameMasterPattern.SpawnPixels(playerRB.position, Vector2.up, 10f, 35);
			
			playerCollider.enabled = false;
			playerRenderer.enabled = false;
			playerRB.velocity = Vector3.zero;
			playerRB.gravityScale = 0f;
			playerRB.mass = 999999999;
		}
	}
}
