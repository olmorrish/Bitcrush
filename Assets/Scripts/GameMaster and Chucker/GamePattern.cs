using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePattern : MonoBehaviour {

    //chucker variables
    private int framesToNextFire;
    public GameObject[] chuckers = new GameObject[4];
    private ChuckerBehavior[] chuckerFireScripts = new ChuckerBehavior[4];
    public PauseMenu pause;

    //game mode variables
    public string fireMode;
   
    //chucker firing pattern variables
    public float minWaitTime;
    public float maxWaitTime;
    private float timeOfNextFire;

    //kill line movement variables
    public GameObject killLine;
    public float killLineSpeed;                           //make larger for higher difficulties!
    private KillLineController killLineController;

    private ScoreData scoreData;
	
	// Use this for initialization
	void Start () {
        for(int i =0; i < chuckers.Length; i++) {
            chuckerFireScripts[i] = chuckers[i].GetComponent<ChuckerBehavior>();
        }

        timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);

        scoreData = GetComponent<ScoreData>();
        killLineController = killLine.GetComponent<KillLineController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(!pause.isPaused && (timeOfNextFire < Time.time)) {
            chuckerFireScripts[Random.Range(0, 4)].Throw(fireMode);
            timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);
        }
    }

    private void FixedUpdate() {
        //current score decides the speed of the kill line
        float score = scoreData.hiScore;
        if (score < 30)
            killLineController.MoveUp(killLineSpeed);
        else if (score < 50)
            killLineController.MoveUp(killLineSpeed * 2.5f);   //TODO: extract doubling calculation to start()
        else
            killLineController.MoveUp(killLineSpeed * 3.2f);
    }
}
