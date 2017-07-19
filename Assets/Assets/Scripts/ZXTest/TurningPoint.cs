using UnityEngine;
using System.Collections;

public class TurningPoint : MonoBehaviour {
	Vector2 initVel = new Vector2 ();

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			Debug.Log ("hey");
			initVel = other.rigidbody2D.velocity;
		}
	}
}
