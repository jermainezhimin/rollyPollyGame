using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {
	private Player player {
		get {
			return manager.activePlayer;
		}
	}
	private PlayerManager manager;

	private static bool isSwitched = false;
	private static float SWITCH_DEAD_ZONE = 0.2f;
	private static float SWITCH_ACTIVE_ZONE = 0.8f;

	private static float previousDeltaAction;

	protected void Start () {
		manager = PlayerManager.INSTANCE;
	}

	protected void Update () {
		UpdateCharacterSwitch ();
		UpdateCharacterMovement ();
		UpdateSecondaryDirection ();
		UpdateJump ();
		UpdateAction ();
	}

	private void UpdateCharacterSwitch () {
		float deltaSwitchAxis = Input.GetAxis ("Switch");

		if (-SWITCH_DEAD_ZONE < deltaSwitchAxis && deltaSwitchAxis < SWITCH_DEAD_ZONE) {
			isSwitched = false;
		}

		if ((!isSwitched && deltaSwitchAxis > SWITCH_ACTIVE_ZONE) || (Input.GetButtonDown ("SwitchNext"))) {
			isSwitched = true;
			manager.Switch (1);
		}

		if (!isSwitched && deltaSwitchAxis < -SWITCH_ACTIVE_ZONE) {
			isSwitched = true;
			manager.Switch (-1);
		}
	}

	private void UpdateCharacterMovement () {
		float deltaX = Input.GetAxis ("Horizontal");
		float deltaY = Input.GetAxis ("Vertical");

		player.Move (deltaX, deltaY);
	}

	private void UpdateSecondaryDirection () {
		float deltaX = Input.GetAxis ("Secondary X");
		float deltaY = Input.GetAxis ("Secondary Y");

		player.SetSecondaryDirection (deltaX, deltaY);
	}

	private void UpdateJump () {
		if (Input.GetButtonDown ("Jump")) {
			player.Jump ();
		}
	}

	private void UpdateAction () {
		// Update Player's ActionDown and Up

		float deltaActionAxis = Input.GetAxis ("Action");

		if (deltaActionAxis < -0.8f) {
			player.Action ();
			if (previousDeltaAction > -0.8f) {
				player.ActionDown ();
			}
		} else {
			if (previousDeltaAction < -0.8f) {
				player.ActionUp ();
			}
		}

		previousDeltaAction = deltaActionAxis;
	}
}