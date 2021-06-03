using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoChuckerScript : MonoBehaviour {

    public ChuckerBehavior[] chuckers;
    float nextChuckTime = Time.time;
    public float timeBetweenChucks;
    int chuckerIndex = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        if(Time.time > nextChuckTime) {
            chuckers[chuckerIndex].Throw("tetromino", false, new BlockPalette("default"));
            chuckerIndex = (chuckerIndex + 1) % chuckers.Length;
            nextChuckTime = Time.time + timeBetweenChucks;
        }

    }
}
