using UnityEngine;
using System.Collections;

public class Player : Character {
	public static Vector3 INVALID_POSITION = new Vector3 (-1000, -1000, -1000);

	public bool isActive;
	public bool isDead;

	protected Vector2 secondaryDirection { get; set; }
	public Vector2 cursor {
		get {
			return (Vector2)transform.position + secondaryDirection;
		}
	}
	public float secondaryRange = 2.0f;

	protected override void Start () {
		base.Start ();

		secondaryDirection = Vector2.zero;
	}

	public virtual void Action () {
	}

	public virtual void ActionDown () {
	}

	public virtual void ActionUp () {
	}

	public void SetSecondaryDirection (float dx, float dy) {
		secondaryDirection += new Vector2 (dx, dy);

		if (secondaryDirection.magnitude > secondaryRange) {
			secondaryDirection = secondaryDirection.normalized * secondaryRange;
		}

		Debug.DrawRay (transform.position, (Vector3)secondaryDirection, Color.green, 0.1f);
	}

	public void Activate (Vector3 currentPosition) {
		isActive = true;
		transform.position = currentPosition;
		rigidbody2D.isKinematic = false;
	}

	public void Deactivate () {
		isActive = false;
		transform.position = INVALID_POSITION;
		rigidbody2D.isKinematic = true;
	}

	public static void Switch (Player playerOff, Player playerOn) {
		Vector3 currentPosition = playerOff.transform.position;
		playerOff.Deactivate ();
		playerOn.Activate (currentPosition);
	}

}
