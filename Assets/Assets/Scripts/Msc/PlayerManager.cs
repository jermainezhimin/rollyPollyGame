using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public static PlayerManager INSTANCE { get; private set; }
	public static int coinCollected { get; set; }

	public Player activePlayer {
		get {
			return players [currentActivePlayerIndex];
		}
	}
	private Player[] players;

	public int currentActivePlayerIndex;

	protected void Awake () {
		if (INSTANCE == null) {
			INSTANCE = this;
		} else {
			Destroy (this);
		}

		players = GetComponentsInChildren <Player> ();

		foreach (Player player in players) {
			player.Deactivate ();
		}

		currentActivePlayerIndex = 0;
		players [currentActivePlayerIndex].Activate (transform.position);

		coinCollected = 0;
	}

	protected void OnDestroy () {
		INSTANCE = null;
	}

	public void Switch (int nextIndex) {
		Player.Switch (players [currentActivePlayerIndex], players [(players.Length + currentActivePlayerIndex + nextIndex) % players.Length]);
		currentActivePlayerIndex = (players.Length + currentActivePlayerIndex + nextIndex) % players.Length;
	}

	public void IgnoreCollision (Collider2D collider, bool status = true) {
		foreach (Player player in players) {
			Physics2D.IgnoreCollision (player.collider2D,
			                           collider,
			                           status);
		}
	}
}