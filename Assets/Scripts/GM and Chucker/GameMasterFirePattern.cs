using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterFirePattern : MonoBehaviour {

	private int framesToNextFire;
	private ChuckerBehavior c1;
	private ChuckerBehavior c2;
	private ChuckerBehavior c3;
	private ChuckerBehavior c4;
	private PauseMenu pause;
	
	public int minWaitTime = 50;
	public int maxWaitTime = 150;
	
	// Use this for initialization
	void Start () {
		c1 = GameObject.Find("Chucker1").GetComponent<ChuckerBehavior>();
		c2 = GameObject.Find("Chucker2").GetComponent<ChuckerBehavior>();
		c3 = GameObject.Find("Chucker3").GetComponent<ChuckerBehavior>();
		c4 = GameObject.Find("Chucker4").GetComponent<ChuckerBehavior>();
		pause = GameObject.Find("PauseCanvas").GetComponent<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!pause.isPaused){
			if (framesToNextFire <= 0){
				Fire();
				framesToNextFire = Random.Range(minWaitTime,maxWaitTime);
			}
			else{
				framesToNextFire -= 1;
			}
		}
	}
	
	
	void Fire(){
		int selector = Random.Range(1,5);
		
		switch(selector){
			case 1: 
				c1.Throw(); break;
			case 2: 
				c2.Throw(); break;
			case 3: 
				c3.Throw(); break;
			case 4: 
				c4.Throw(); break;
			default: break;
			
		}
		
	}
}
