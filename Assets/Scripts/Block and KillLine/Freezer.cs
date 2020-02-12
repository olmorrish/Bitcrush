using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour {

	public bool frozen; //used to check when player collides - player explodes if not frozen

    public AudioSource[] hitFX = new AudioSource[2];
    private Rigidbody2D blockRB;

    private SpriteRenderer blockRend;
    [Range(0, 1)] public float dullHueTolerance= 0.52f;
    [Range(0, 1)] public float dullPercentageOnFreeze = 0.4f;

    void Awake () {
        frozen = false;

		blockRB = GetComponent<Rigidbody2D>();
        blockRend = GetComponent<SpriteRenderer>();
        hitFX[0] = GameObject.Find("HitFX1").GetComponent<AudioSource>();
        hitFX[1] = GameObject.Find("HitFX2").GetComponent<AudioSource>();
    }
	
	void Update(){
        //if not moving, freeze
		if(blockRB.velocity.magnitude < 0.01f && !frozen){
			Freeze();
		}
	}


    public void Freeze(){
        frozen = true;

        hitFX[Random.Range(0, hitFX.Length - 1)].Play();
		blockRB.constraints = RigidbodyConstraints2D.FreezeAll;
		blockRB.velocity = Vector3.zero;

        blockRend.color = GreyOut(blockRend.color, dullHueTolerance, dullPercentageOnFreeze);    //reduces hues within 52% of the highest hue to 40% of their value
    }	

    /*
     * tolerance is how near a hue must be to the highest RGB hue to be reduced (from 0 to 1)
     * dullStrength is the percentage by which relevant hues will be reduced
     */
    private Color GreyOut(Color c, float tolerance, float dullStrength) {

        float[] hueValues = new float[3];   //colour mod is done in-place in this array
        hueValues[0] = c.r;
        hueValues[1] = c.g;
        hueValues[2] = c.b;

        float maxHue = Mathf.Max(hueValues);

        for (int i = 0; i < 3; i++) {
            float offsetFromMaxHue = Mathf.Abs(maxHue - hueValues[i]);
            if(offsetFromMaxHue <= tolerance) {
                hueValues[i] = (hueValues[i] * (1-dullStrength));     //if the hue is within a certain range of the highest hue, dull it
            }
        }

        return new Color(hueValues[0], hueValues[1], hueValues[2]);
    }
}
