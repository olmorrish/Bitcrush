using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool isPaused = false;
    public GameObject thePauseMenu;
    private ScoreData scoreData;
    private GameOverMenu gameOverMenu;

    // Use this for initialization
    void Awake() {
        thePauseMenu = GameObject.Find("PauseMenu");
        thePauseMenu.SetActive(false);
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenu>();
        Resume();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Start")) {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume() {

        thePauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Retry() {
        gameOverMenu.PostGameWrapUp();
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Pause() {

        thePauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Exit() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}