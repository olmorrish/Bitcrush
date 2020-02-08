using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreData : MonoBehaviour{

    private float height;
    public float hiScore;

    public Text gameplayScore;
    public Text gameOverScore;

    public GameObject player;
    public GameObject scoreLine;

    // Start is called before the first frame update
    void Start() {
        hiScore = 0;
        PushNewHiScore(0f);
    }

    // Update is called once per frame
    void Update() {
        height = player.transform.position.y;
        if (height > hiScore) {
            hiScore = height;
            PushNewHiScore(hiScore);
        }
    }

    void PushNewHiScore(float score) {
        gameplayScore.text = "SCORE: " + Mathf.Ceil(score).ToString();
        gameOverScore.text = "FiNAL SCORE: " + Mathf.Ceil(score).ToString();
        scoreLine.transform.position = new Vector3(0, score, 0);
    }
}
