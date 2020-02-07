using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterFirePattern : MonoBehaviour {

	private int framesToNextFire;
    public GameObject[] chuckers = new GameObject[4];
    private ChuckerBehavior[] chuckerFireScripts = new ChuckerBehavior[4];
	public PauseMenu pause;
	
	public int minWaitTime = 50;
	public int maxWaitTime = 150;
	
	// Use this for initialization
	void Start () {
        for(int i =0; i < chuckers.Length; i++) {
            chuckerFireScripts[i] = chuckers[i].GetComponent<ChuckerBehavior>();
        }
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
		int pick = Random.Range(1,5);
        chuckerFireScripts[pick].Throw();
	}
}
