using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Specifically used for the post-processing effects
 */
public class PersistOnSceneChange : MonoBehaviour{

// Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject); //ensures the objects always persists
    }
}
