using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDespawnBehavior : MonoBehaviour {

	private Rigidbody2D rb;
	public float fallSpeedToDespawn;

    // Use this for initialization
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (rb.velocity.y < -fallSpeedToDespawn) {
            Destroy(gameObject);
        }
    }
}
