using UnityEngine;
using System.Collections;

public class RollyPolly : Player {
	protected GameObject ground;
	private float potentialEnergy, maxXVel;
	public float flatGroundPEAttenuation = 0.2f; 
	public float scalingPEAttenuation = 0.8f;
	public float maxSpeed = 60f;
	public float peStoreCeil = 20f;
	public float peMultiplier = 5f;
	public float impulseBoost = 10f;
	public float minSpeed = 15f;

	public float velocityDropThreshold = 0.3f;
	private Vector2 accumulatedForce, accumulatedImpulse, velMultiplier;

	private Vector3 prevPosition, prevVelocity;
	bool isTurningPoint = false;

	public Vector2 velocity; //FOR TESTING
	private Transform prevGround;
	public float peStore;


	public float transitionThreshold = 0.5f;
	public override void Move (float dx, float dy) {
//		if (dx == 0 && rigidbody2D.velocity.magnitude < 0.3) {
//			peStore = 0;
//		} else {
//			accumulatedForce = Vector2.right * moveSpeed * dx + velocityVector * peMultiplier * peStore * dx;
		accumulatedForce = velocityVector * moveSpeed * dx;
		DetermineImpulse (dx);
		//}


	}

	void LimitSpeed () {
		if (rigidbody2D.velocity.magnitude > maxSpeed) {
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
		}
	}

//	if (Mathf.Abs (rigidbody2D.velocity.x) < minSpeed) {
//		accumulatedImpulse = impulseBoost * velocityVector * dx;
//	}
	protected override void Update () {
		base.Update ();


		//Testing
		velocity = rigidbody2D.velocity; 
	}
	protected override void FixedUpdate () {
		LimitSpeed ();

		SetPE ();
		rigidbody2D.AddForce (accumulatedForce);
		//Debug.Log (prevVelocity.magnitude - rigidbody2D.velocity.magnitude);
		rigidbody2D.AddForce (accumulatedImpulse, ForceMode2D.Impulse);
		accumulatedForce = Vector2.zero;
		accumulatedImpulse = Vector2.zero;

		ground = null;
		prevPosition = transform.position;
		prevVelocity = rigidbody2D.velocity;
		velocityVector = Vector3.right;

	}
	static float Sq (float n) {
		return n * n;
	}
	void DetermineImpulse (float dx) {
		Vector3 currVel = rigidbody2D.velocity;
		if (currVel.magnitude < prevVelocity.magnitude &&
			prevVelocity.magnitude - currVel.magnitude > velocityDropThreshold) {
			float diff = prevVelocity.magnitude - currVel.magnitude;
			Debug.Log ("Difference is : " + diff);
			accumulatedImpulse = velocityVector * (float)(0.5 * rigidbody2D.mass * 
				(Sq (prevVelocity.magnitude) - Sq (currVel.magnitude)) /
				(prevPosition - transform.position).magnitude * dx);

		}
	}

	protected void OnCollisionStay2D (Collision2D collision) {
		int groundLayer = LayerMask.NameToLayer ("Ground");
		if (collision.gameObject.layer == groundLayer) {
			ground = collision.gameObject;
			velocityVector = collision.transform.right.normalized;
			if (collision.transform != prevGround && collision.transform != null && prevGround != null) {
				velocityVector = PlaneAhead (rigidbody2D.velocity, collision.transform.position, velocityVector,
				                            prevGround.position, prevGround.right.normalized);
			}
			prevGround = collision.transform;
		}

	}

//	void AddAccumulatedImpulse (float dx) {
//		if (peStore > 0) {
//			accumulatedImpulse = velocityVector * peMultiplier * Mathf.Min (peStore, peStoreCeil) * dx;
//			Debug.Log ("Impulse is " + accumulatedImpulse);
//		}
//	}


	Vector3 PlaneAhead (Vector3 movement, Vector3 planeAPos, Vector3 planeARight, Vector3 planeBPos, Vector3 planeBRight) {
		if (movement.x > 0) {
			if (planeAPos.x > planeBPos.x) {
				return planeARight;
			} else {
				return planeBRight;
			}
		} else {
			if (planeAPos.x < planeBPos.x) {
				return planeARight;
			} else {
				return planeBRight;
			}
		}
	}
	void SetPE () {
		float prevY = prevPosition.y;
		if (prevY - transform.position.y > 0) {
			peStore += prevY - transform.position.y;
		} else if (prevY == transform.position.y) {
			peStore -= flatGroundPEAttenuation * Time.deltaTime;
		} else {
			peStore -= scalingPEAttenuation * Time.deltaTime;
		}
		peStore = Mathf.Max (peStore, 0);
		peStore = Mathf.Min (peStore, peStoreCeil);
	}

}
