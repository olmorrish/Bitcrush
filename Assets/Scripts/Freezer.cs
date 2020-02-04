using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour {

	public bool frozen;			//indicates whether Freeze() has been called on the block
	public Vector3 frozenPos;
	
	private Rigidbody2D rb;
	
	private AudioSource hit1;
	private AudioSource hit2;
	private AudioSource hit3;
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		hit1 = GameObject.Find("HitFX1").GetComponent<AudioSource>();
		hit2 = GameObject.Find("HitFX3").GetComponent<AudioSource>();
		hit3 = GameObject.Find("HitFX3").GetComponent<AudioSource>();
	}

	void OnCollisionEnter2D(Collision2D col){	
		if(col.gameObject.tag != "Player"){
			HitSound();
		}
	}
	
	void Update(){
		if(frozen){
			transform.position = frozenPos;
		}
		
		if(rb.velocity == Vector2.zero && !frozen){
			Freeze();
		}
	}
	
	
	public void Freeze(){
		
		HitSound();
		
		frozen = true;
		frozenPos = transform.position;
		
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		rb.velocity = Vector3.zero;
	}

	
	
	
	void HitSound(){
		int soundSelect = Random.Range(0,3);
		
		switch(soundSelect){
			case 0:
				hit1.Play(); break;
			case 1:
				hit2.Play(); break;
			case 2:
				hit3.Play(); break;
			default:
				break;
		}
	}
	
	
	
}
