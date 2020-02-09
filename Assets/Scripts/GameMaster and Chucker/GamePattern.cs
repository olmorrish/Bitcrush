using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePattern : MonoBehaviour {

    //chucker variables
    private int framesToNextFire;
    public GameObject[] chuckers = new GameObject[4];
    private ChuckerBehavior[] chuckerFireScripts = new ChuckerBehavior[4];
    public PauseMenu pause;
    private ScoreData scoreData;

    //game mode variables
    public string fireMode;
    public bool rotateBy45Degrees;

    //chucker firing pattern variables
    public float minWaitTime;
    public float maxWaitTime;
    private float maxWaitTimeStage2;
    private float maxWaitTimeStage3;
    private float timeOfNextFire;

    //kill line movement variables
    public GameObject killLine;
    public float killLineSpeed = 1.3f;                           //make larger for higher difficulties!
    private KillLineController killLineController;

	// Use this for initialization
	void Start () {
        for(int i =0; i < chuckers.Length; i++) {
            chuckerFireScripts[i] = chuckers[i].GetComponent<ChuckerBehavior>();
        }

        timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);
        maxWaitTimeStage2 = maxWaitTime - ((maxWaitTime - minWaitTime) * 0.25f);    //3/4s the possible interval
        maxWaitTimeStage3 = maxWaitTime - ((maxWaitTime - minWaitTime) * 0.5f);     //halves the possible interval 

        scoreData = GetComponent<ScoreData>();
        killLineController = killLine.GetComponent<KillLineController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(!pause.isPaused && (timeOfNextFire < Time.time)) {   //time to fire!
            chuckerFireScripts[Random.Range(0, chuckers.Length)].Throw(fireMode, rotateBy45Degrees);

            //reset timer based on progress
            if(scoreData.hiScore < 30)
                timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);
            else if (scoreData.hiScore < 50)
                timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTimeStage2);
            else
                timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTimeStage3);
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
