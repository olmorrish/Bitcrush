using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public bool isGameOver = false; 
	private GameObject theGameOverMenu;
	public PauseMenu thePauseMenu;

    public GameObject gameMaster;
    private ScoreData scoreData;
    public GameObject player;
	private Exploder exploder;
    public GameObject musicObject;
	private AudioSource musicTrack;
    private Boost playerBoost;

    // Use this for initialization
    void Start () {
		theGameOverMenu = GameObject.Find("GameOverMenu");                      // Thanks, 2018-me - don't know how this works but it does. Sincerely, 2020-me.
		theGameOverMenu.SetActive(false);

		exploder = player.GetComponent<Exploder>();
        playerBoost = player.GetComponent<Boost>();
        musicTrack = musicObject.GetComponent<AudioSource>();

        scoreData = gameMaster.GetComponent<ScoreData>();

		isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(exploder.exploded && !isGameOver){
			GameOver();
			isGameOver = true;
		}
		
		//don't allow player to pause if on GO screen
		if(isGameOver && thePauseMenu.isPaused){
			thePauseMenu.Resume();
		}
	}
	
	/*
	 * Activates Gameover
	 */
	public void GameOver(){
        //enable the menu
		theGameOverMenu.SetActive(true);
		musicTrack.volume = (0.5f) * musicTrack.volume;
        playerBoost.boostEnabled = false;

        //block unfreeze
        GameObject[] allBlocksInScene = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in allBlocksInScene) {
            block.GetComponent<Freezer>().UnFreeze();
            block.GetComponent<Freezer>().hasntBeenFrozenYet = false;    //stops them from freezing for a first time after gameover
        }

        //highscorescore saves update, game count increment, unlocks 
        scoreData.GameOverSaveHighScore();
        IncrementGameCount();
        CheckForUnlocksAndQueueMsgs(); //new highscores and game counter are current

    }

    /* Check for Unlocks, Queue Messages
     * Runs through all unlock conditions, the unlocks any that the player has met the requirements for. 
     * If no unlocks were achieved, queues a hint about what to try next instead, which is based on what has already been unlocked. 
     */
    private void CheckForUnlocksAndQueueMsgs() {

        //get info required for unlock decisions
        PersistentSettings persistentSettings = GameObject.Find("PersistentSettingsObject").GetComponent<PersistentSettings>();
        GameMode currentMode = persistentSettings.currentModeName;
        float score = scoreData.localHighScore;
        int numGamesPlayed = PlayerPrefs.GetInt("numGamesPlayed", 0);
        int highscoreSum = GetHighscoreSum();

        #region Unlock Checks and Unlock Messages
        //Casual Mode + Pastel Palette (1 game played)
        if (PlayerPrefs.GetInt("UL_Casual", 0) == 0) {
            PlayerPrefs.SetInt("UL_Casual", 1);
            persistentSettings.unlockMessageQueue.Add("CASUAL MODE UNLOCKED");
        }
        if(PlayerPrefs.GetInt("PAL_Pastel", 0) == 0) {
            PlayerPrefs.SetInt("PAL_Pastel", 1);
            persistentSettings.unlockMessageQueue.Add("PASTEL PALETTE UNLOCKED");
        }

        //Tromino Mode + Warm Palette (>25 points in Normal Mode)
        if(PlayerPrefs.GetInt("UL_Tromino") == 0 && currentMode == GameMode.Normal && score >= 25) {
            PlayerPrefs.SetInt("UL_Tromino", 1);
            PlayerPrefs.SetInt("PAL_Warm", 1);
            persistentSettings.unlockMessageQueue.Add("TROMiNO MODE UNLOCKED");
            persistentSettings.unlockMessageQueue.Add("WARM PALETTE UNLOCKED");
        }

        //Cool Palette (>50 points in Tromino Mode)
        if(PlayerPrefs.GetInt("PAL_Cool", 0) == 0 && currentMode == GameMode.Tromino && score >= 50){
            PlayerPrefs.SetInt("PAL_Cool", 1);
            persistentSettings.unlockMessageQueue.Add("COOL PALETTE UNLOCKED");
        }

        //University Sim. (<5 points)
        if (PlayerPrefs.GetInt("UL_UniversitySim", 0) == 0 && score < 5) {
            PlayerPrefs.SetInt("UL_UniversitySim", 1);
            persistentSettings.unlockMessageQueue.Add("UNiVERSiTY SiM MODE UNLOCKED");
        }

        //Pentomino Mode (>30 points in both normal and tromino mode)
        if (PlayerPrefs.GetInt("UL_Pentomino", 0) == 0 && PlayerPrefs.GetInt("HS_Normal", 0) >= 30 && PlayerPrefs.GetInt("HS_Tromino", 0) >= 30) {
            PlayerPrefs.SetInt("UL_Pentomino", 1);
            persistentSettings.unlockMessageQueue.Add("PENTOMiNO MODE UNLOCKED");
        }

        if (PlayerPrefs.GetInt("PAL_Monochrome", 0) == 0) {
            if(highscoreSum >= 150) {
                PlayerPrefs.SetInt("PAL_Monochrome", 1);
                persistentSettings.unlockMessageQueue.Add("MONOCHROME PALETTE UNLOCKED");
            }
        }

        if(PlayerPrefs.GetInt("UL_Tiny", 0) == 0) {
            if(numGamesPlayed >= 15) {
                PlayerPrefs.SetInt("UL_Tiny", 1);
                persistentSettings.unlockMessageQueue.Add("TiNY BLOCKS MODE UNLOCKED");
            }
        }

            //TODO
            #endregion

            #region Unlock Hints
            if (persistentSettings.unlockMessageQueue.Count < 1) {
            persistentSettings.unlockMessageQueue.Add("GET 200 POiNTS iN NORMAL MODE FOR A NEW GAME MODE");
        }
        #endregion
    }

    /* Increment Games Count
     */
    private void IncrementGameCount() {
        int count = PlayerPrefs.GetInt("numGamesPlayed",0);
        PlayerPrefs.SetInt("numGamesPlayed", count + 1);
    }

    private int GetHighscoreSum() {
        int sum = PlayerPrefs.GetInt("HS_Normal", 0);
        sum += PlayerPrefs.GetInt("HS_Tromino", 0);
        sum += PlayerPrefs.GetInt("HS_Pentomino", 0);
        sum += PlayerPrefs.GetInt("HS_Tiny", 0);
        sum += PlayerPrefs.GetInt("HS_Bitcrusher", 0);
        sum += PlayerPrefs.GetInt("HS_SlipperySlopes", 0);
        sum += PlayerPrefs.GetInt("HS_UpsideDown", 0);
        sum += PlayerPrefs.GetInt("HS_UniversitySim", 0);
        sum += PlayerPrefs.GetInt("HS_Bitwarped", 0);
        sum += PlayerPrefs.GetInt("HS_Bitcrusher2", 0);
        sum += PlayerPrefs.GetInt("HS_Impossible", 0);
        return sum;
    }


    /*
	 * Loads scene again
	 */
    public void Retry(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
	
	/*
	 * returns to main menu
	 */
	public void Exit(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}

}
