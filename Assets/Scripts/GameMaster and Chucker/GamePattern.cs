using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePattern : MonoBehaviour {

    //gamestate references
    public GameOverMenu gameOverMenu;
    public GameObject pixelObj;

    //chucker variables
    private int framesToNextFire;
    public GameObject[] chuckers = new GameObject[4];
    private ChuckerBehavior[] chuckerFireScripts = new ChuckerBehavior[4];
    public PauseMenu pause;
    private ScoreData scoreData;

    //game mode variables
    public string fireMode;
    [HideInInspector] public bool rotateBy45Degrees;
    [HideInInspector] public BlockPalette currentPalette;

    //chucker firing pattern variables
    [HideInInspector] public float minWaitTime;
    [HideInInspector] public float maxWaitTime;
    private float maxWaitTimeStage2;
    private float maxWaitTimeStage3;
    private float timeOfNextFire;

    //kill line movement variables
    public GameObject killLine;
    public float killLineSpeed = 1.3f;                           //make larger for higher difficulties!
    private KillLineController killLineController;

	// Use this for initialization
	void Start () {
        for (int i =0; i < chuckers.Length; i++) {
            chuckerFireScripts[i] = chuckers[i].GetComponent<ChuckerBehavior>();
        }

        timeOfNextFire = Time.time + Random.Range(minWaitTime, maxWaitTime);
        maxWaitTimeStage2 = maxWaitTime - ((maxWaitTime - minWaitTime) * 0.25f);    //3/4s the possible interval
        maxWaitTimeStage3 = maxWaitTime - ((maxWaitTime - minWaitTime) * 0.5f);     //halves the possible interval 

        scoreData = GetComponent<ScoreData>();
        killLineController = killLine.GetComponent<KillLineController>();

        if (currentPalette == null)             //this will only every be null when debugging - when the menu was not launched before the game scene
            currentPalette = new BlockPalette();   
    }
	
	// Update is called once per frame
	void Update () {
		if(!pause.isPaused && !gameOverMenu.isGameOver && (timeOfNextFire < Time.time) ) {   //time to fire!

            chuckerFireScripts[Random.Range(0, chuckers.Length)].Throw(fireMode, rotateBy45Degrees, currentPalette);

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
        if (!gameOverMenu.isGameOver) {
            float score = scoreData.hiScore;
            if (score < 30)
                killLineController.MoveUp(killLineSpeed);
            else if (score < 50)
                killLineController.MoveUp(killLineSpeed * 2.5f);   //TODO: extract doubling calculation to start()
            else
                killLineController.MoveUp(killLineSpeed * 3.2f);
        }
        
    }

    public void SpawnPixels(Vector2 where, Vector2 whichDirection, float randomFactor, int howMany) {
        for (int i = 0; i < howMany; i++) {
            GameObject pixelClone = (GameObject)Instantiate(pixelObj, where, transform.rotation);

            //decide colour of pixel
            Color pixColor = currentPalette.palette[Random.Range(0,7)];
            pixelClone.GetComponent<SpriteRenderer>().color = pixColor;

            //decide where the pixel is flying
            float xtraj = whichDirection.x + Random.Range(-randomFactor, randomFactor);
            float ytraj = whichDirection.y + Random.Range(-randomFactor, randomFactor);
            Vector3 trajectory = new Vector2(xtraj, ytraj);
            pixelClone.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
        }
    }
}

