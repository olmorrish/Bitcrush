using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomGamesMenu : MonoBehaviour {

    public GameObject settingsObject;
    private PersistentSettings settings;

    // Start is called before the first frame update
    void Start() {
        settings = settingsObject.GetComponent<PersistentSettings>();
    }

    private void LoadGame() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
