using UnityEngine;
using System.Collections;

public class Character : WorldObject {
	private GroundDetector groundDetector;

	public float moveSpeed = 5.0f;
	public float jumpForce = 5.0f;

	protected Vector2 velocityVector;

	protected override void Start () {
		base.Start ();

		groundDetector = GetComponentInChildren <GroundDetector> ();
	}

	public virtual void Move (float dX, float dY) {
		velocityVector = new Vector2 (dX * moveSpeed, rigidbody2D.velocity.y);
		rigidbody2D.velocity = velocityVector;
	}

	public virtual void Jump () {
		if (groundDetector.isGrounded) {
			velocityVector = new Vector2 (rigidbody2D.velocity.x, jumpForce);
			rigidbody2D.velocity = velocityVector;
		}
	}

}
