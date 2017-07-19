using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	protected void OnTriggerEnter2D (Collider2D collider) {
		Player player = collider.GetComponent< Player > ();
		if (player != null) {
			PlayerManager.coinCollected ++;
			Destroy (gameObject);
		}
	}
}