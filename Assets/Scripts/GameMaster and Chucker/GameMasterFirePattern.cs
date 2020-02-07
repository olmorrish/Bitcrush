using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterFirePattern : MonoBehaviour {

	private int framesToNextFire;
    public GameObject[] chuckers = new GameObject[4];
    private ChuckerBehavior[] chuckerFireScripts = new ChuckerBehavior[4];
	public PauseMenu pause;

    public float minWaitTime;
    public float maxWaitTime;
    private float timeOfNextFire;

    //public int minWaitTime = 50;
	//public int maxWaitTime = 150;
	
	// Use this for initialization
	void Start () {
        for(int i =0; i < chuckers.Length; i++) {
            chuckerFireScripts[i] = chuckers[i].GetComponent<ChuckerBehavior>();
        }

        timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);

    }
	
	// Update is called once per frame
	void Update () {
		if(!pause.isPaused && (timeOfNextFire < Time.time)) {
            chuckerFireScripts[Random.Range(0, 4)].Throw();
            timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);
        }
	}
}
