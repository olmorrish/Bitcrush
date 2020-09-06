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
            PlayerPrefs.SetInt("PAL_Pastel", 1);
            persistentSettings.unlockMessageQueue.Add("CASUAL MODE UNLOCKED");
            persistentSettings.unlockMessageQueue.Add("PASTEL PALETTE UNLOCKED");
        }

        //Tromino Mode + Warm Palette (>25 points in Normal Mode)
        if(PlayerPrefs.GetInt("UL_Tromino") == 0 && currentMode == GameMode.Normal && score >= 25) {
            PlayerPrefs.SetInt("UL_Tromino", 1);
            PlayerPrefs.SetInt("PAL_Warm", 1);
            persistentSettings.unlockMessageQueue.Add("TROMiNO MODE UNLOCKED");
            persistentSettings.unlockMessageQueue.Add("WARM PALETTE UNLOCKED");
        }

        //Retrowave Palette (>128 points in Normal Mode)
        if (PlayerPrefs.GetInt("UL_Retrowave") == 0 && currentMode == GameMode.Normal && score >= 128) {
            PlayerPrefs.SetInt("UL_Retrowave", 1);
            persistentSettings.unlockMessageQueue.Add("RETROWAVE PALETTE UNLOCKED");
        }

        //Cool Palette (>50 points in Tromino Mode)
        if (PlayerPrefs.GetInt("PAL_Cool", 0) == 0 && currentMode == GameMode.Tromino && score >= 50){
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

        //Palewave Palette (>75 points in normal, tromino, and pentomino mode)
        if (PlayerPrefs.GetInt("PAL_Palewave", 0) == 0 && PlayerPrefs.GetInt("HS_Normal", 0) >= 75 && PlayerPrefs.GetInt("HS_Tromino", 0) >= 75 && PlayerPrefs.GetInt("HS_Pentomino", 0) >= 75){
            PlayerPrefs.SetInt("PAL_Palewave", 1);
            persistentSettings.unlockMessageQueue.Add("PALEWAVE PALETTE UNLOCKED");
        }

        //Slippery Slopes Mode + Mountain Palette (>100 points in pentomino mode)
        if (PlayerPrefs.GetInt("UL_SlipperySlopes") == 0 && currentMode == GameMode.Pentomino && score >= 100) {
            PlayerPrefs.SetInt("UL_SlipperySlopes", 1);
            PlayerPrefs.SetInt("PAL_Mountain", 1);
            persistentSettings.unlockMessageQueue.Add("SLiPPERY SLOPES MODE UNLOCKED");
            persistentSettings.unlockMessageQueue.Add("MOUNTAiN PALETTE UNLOCKED");
        }

        //Bitwarped Mode (>200 points in any noncausual mode)
        if (PlayerPrefs.GetInt("UL_Bitwarped") == 0 && currentMode != GameMode.Casual && score >= 200) {
            PlayerPrefs.SetInt("UL_Bitwarped", 1);
            persistentSettings.unlockMessageQueue.Add("BiTWARPED MODE UNLOCKED");
        }

        //Bitcrusher II Mode (>150 points in bitcrusher mode)
        if (PlayerPrefs.GetInt("UL_Bitcrusher2") == 0 && currentMode == GameMode.Bitcrusher && score >= 150) {
            PlayerPrefs.SetInt("UL_Bitcrusher2", 1);
            persistentSettings.unlockMessageQueue.Add("BiTCRUSHER II MODE UNLOCKED");
        }

        //Monochrome Palette (>150 highscore sum)
        if (PlayerPrefs.GetInt("PAL_Monochrome", 0) == 0) {
            if(highscoreSum >= 150) {
                PlayerPrefs.SetInt("PAL_Monochrome", 1);
                persistentSettings.unlockMessageQueue.Add("MONOCHROME PALETTE UNLOCKED");
            }
        }

        //Forest Palette (>250 highscore sum)
        if (PlayerPrefs.GetInt("PAL_Forest", 0) == 0) {
            if (highscoreSum >= 250) {
                PlayerPrefs.SetInt("PAL_Forest", 1);
                persistentSettings.unlockMessageQueue.Add("FOREST PALETTE UNLOCKED");
            }
        }

        //Pride Palette (>400 highscore sum)
        if (PlayerPrefs.GetInt("PAL_Pride", 0) == 0) {
            if (highscoreSum >= 400) {
                PlayerPrefs.SetInt("PAL_Pride", 1);
                persistentSettings.unlockMessageQueue.Add("PRiDE PALETTE UNLOCKED");
            }
        }

        //Upside Down Mode (>600 highscore sum)
        if (PlayerPrefs.GetInt("UL_UpsideDown", 0) == 0) {
            if (highscoreSum >= 600) {
                PlayerPrefs.SetInt("UL_UpsideDown", 1);
                persistentSettings.unlockMessageQueue.Add("UPSiDE DOWN MODE UNLOCKED");
            }
        }

        //Impossible Mode (>1000 highscore sum)
        if (PlayerPrefs.GetInt("UL_Impossible", 0) == 0) {
            if (highscoreSum >= 1000) {
                PlayerPrefs.SetInt("UL_Impossible", 1);
                persistentSettings.unlockMessageQueue.Add("iMPOSSiBLE MODE UNLOCKED");
            }
        }

        //Tiny Blocks (play 15 games)
        if (PlayerPrefs.GetInt("UL_Tiny", 0) == 0) {
            if(numGamesPlayed >= 15) {
                PlayerPrefs.SetInt("UL_Tiny", 1);
                persistentSettings.unlockMessageQueue.Add("TiNY BLOCKS MODE UNLOCKED");
            }
        }

        //Bitcrusher Mode (play 30 games)
        if (PlayerPrefs.GetInt("UL_Bitcrusher", 0) == 0) {
            if (numGamesPlayed >= 30) {
                PlayerPrefs.SetInt("UL_Bitcrusher", 1);
                persistentSettings.unlockMessageQueue.Add("BiTCRUSHER MODE UNLOCKED");
            }
        }

        //Random Palette (play 50 games)
        if (PlayerPrefs.GetInt("PAL_Random", 0) == 0) {
            if (numGamesPlayed >= 50) {
                PlayerPrefs.SetInt("PAL_Random", 1);
                persistentSettings.unlockMessageQueue.Add("RANDOM PALETTE UNLOCKED");
            }
        }

        //TODO Remaining Unlocks
        #endregion

        #region Unlock Hints
        if (persistentSettings.unlockMessageQueue.Count == 0) {

            System.Random rand = new System.Random();
            List<string> possibleHintMessages = new List<string>();

            //Normal Mode Unlocks
            if(PlayerPrefs.GetInt("PAL_Retrowave", 0) == 0) {
                possibleHintMessages.Add("GET 128 POiNTS in NORMAL MODE TO UNLOCK A RETRO PALETTE");
            }

            //Highscore sum unlock hints
            if(highscoreSum < 150) {
                possibleHintMessages.Add("GET A TOTAL HiGHSCORE OF 150 ACROSS ALL NON-CASUAL MODES TO UNLOCK AN OLD-TiMEY PALETTE");
            }
            else if(highscoreSum < 250) {
                possibleHintMessages.Add("GET A TOTAL HiGHSCORE OF 250 ACROSS ALL NON-CASUAL MODES TO UNLOCK A NATURAL PALETTE");
            }
            else if (highscoreSum < 400) {
                possibleHintMessages.Add("GET A TOTAL HiGHSCORE OF 400 ACROSS ALL NON-CASUAL MODES TO UNLOCK A PROUD PALETTE");
            }
            else if (highscoreSum < 600) {
                possibleHintMessages.Add("GET A TOTAL HiGHSCORE OF 600 ACROSS ALL NON-CASUAL MODES TO UNLOCK A ZANY GAME MODE");
            }
            else if (highscoreSum < 1000) {
                possibleHintMessages.Add("GET A TOTAL HiGHSCORE OF 1000 ACROSS ALL NON-CASUAL MODES TO UNLOCK AN iMPOSSIBLE GAME MODE");
            }

            //Games played unlock hints
            if(numGamesPlayed < 15) {
                possibleHintMessages.Add("PLAY 15 GAMES TO UNLOCK A MiCROSCOPiC GAME MODE");
            }
            else if (numGamesPlayed < 30) {
                possibleHintMessages.Add("PLAY 30 GAMES TO UNLOCK A DiFFiCULT GAME MODE");
            }
            else if (numGamesPlayed < 50) {
                possibleHintMessages.Add("PLAY 50 GAMES TO UNLOCK A WiLD PALETTE");
            }

            //Tromino Mode Gate
            if (PlayerPrefs.GetInt("UL_Tromino", 0) == 0) {
                possibleHintMessages.Add("GET 25 POiNTS iN NORMAL MODE TO UNLOCK A NEW GAME MODE");
            }
            else {
                possibleHintMessages.Add("GET 50 POiNTS iN TROMiNO MODE TO UNLOCK A CHiLLY PALETTE");
                possibleHintMessages.Add("GET HiGHSCORES OF 30 iN NORMAL MODE AND TROMiNO MODE TO UNLOCK AN APROPOS GAME MODE");
            }

            //Pentomino Gate (University Sim hint also locked here)
            if (PlayerPrefs.GetInt("UL_Pentomino", 0) == 1) {
                possibleHintMessages.Add("GET 75 POiNTS iN NORMAL, TROMiNO, AND PENTOMiNO MODE TO UNLOCK A TRENDY PALETTE");
                possibleHintMessages.Add("GET 100 POiNTS iN PENTOMiNO MODE TO UNLOCK A TREACHEROUS GAME MODE");
                possibleHintMessages.Add("PERFORM TERRiBLY TO UNLOCK AN APPROPRiATE GAME MODE");
            }

            //Bitcrusher Gate (Bitwarped hint also locked here)
            if (PlayerPrefs.GetInt("UL_Bitcrusher", 0) == 1) {
                possibleHintMessages.Add("GET 150 POiNTS in BiTCRUSHER MODE TO UNLOCK A PUNiSHING GAME MODE");
                possibleHintMessages.Add("GET 200 POiNTS iN ANY NON-CASUAL MODE TO UNLOCK A BiZARRE GAME MODE");
            }

            string hintMessageSelected = possibleHintMessages[rand.Next(0, possibleHintMessages.Count)];
            persistentSettings.unlockMessageQueue.Add(hintMessageSelected);
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
