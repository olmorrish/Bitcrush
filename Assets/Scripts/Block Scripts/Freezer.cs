using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour {

	public bool frozen; //used to check when player collides - player explodes if not frozen

	private Rigidbody2D rb;
    public AudioSource[] hitFX = new AudioSource[2];

    void Awake () {
        frozen = false;

		rb = GetComponent<Rigidbody2D>();
        hitFX[0] = GameObject.Find("HitFX1").GetComponent<AudioSource>();
        hitFX[1] = GameObject.Find("HitFX2").GetComponent<AudioSource>();
    }
	
	void Update(){
        //if not moving, freeze
		if(rb.velocity.magnitude < 0.01f && !frozen){
			Freeze();
		}
	}
	
	
	public void Freeze(){
        frozen = true;

        hitFX[Random.Range(0, hitFX.Length - 1)].Play();
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		rb.velocity = Vector3.zero;
	}	
}
