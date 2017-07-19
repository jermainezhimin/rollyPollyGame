using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	Edible edible;
	protected void Start () {
		edible = gameObject.GetComponent<Edible> ();
	}
	void OnCollisionEnter2D (Collision2D other) {
		if (!edible.GetEaten ()) {
			if (other.gameObject.layer == LayerMask.NameToLayer ("Player")) {
				other.gameObject.SetActive (false);
			} else if (other.gameObject.GetComponent<Edible> () != null) {
				GetsShot ();
			}
		}
	}
	public void GetsShot () {
		gameObject.SetActive (false);
	}
}
