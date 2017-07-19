using UnityEngine;
using System.Collections;

public class Munchy : Player {
	float eatingDistance = 2.0f; //To be replaced
	readonly int indexOfTongueChild = 1;
	Vector2 eatingDirection; //Translated secondary direction to where tongue is
	Transform tongueTransform;
	LayerMask nonmunchyLayer;
	GameObject eatenFood = null;
	public float spittingForce = 20.0f;
	protected override void Start () {
		base.Start ();
		nonmunchyLayer = ~(1 << LayerMask.NameToLayer ("Player"));
		tongueTransform = gameObject.GetComponentsInChildren<Transform> () [indexOfTongueChild];
		SetEatingDirection ();
		Vector3 newScale = new Vector3 (tongueTransform.localScale.x, tongueTransform.localScale.y, eatingDistance);
		tongueTransform.localScale = newScale;

	}

	protected override void Update () {
		base.Update ();
		SetEatingDirection ();
		Debug.DrawRay (tongueTransform.position, 
		               eatingDirection.normalized * eatingDistance, Color.red);
		Flip ();
		RotateTongue ();

	}
	void Flip () {
		float dotProduct = Vector3.Dot (secondaryDirection.normalized, transform.right);
		if (dotProduct < 0) {
			transform.Rotate (0, 180, 0);
		}
	}

	void RotateTongue () {
		tongueTransform.LookAt (secondaryDirection + (Vector2)transform.position);
	}

	public override void ActionDown () {

		if (eatenFood != null) {
			Spit ();
		} else {
			Eat ();

		}

	}

	void SetEatingDirection () {
		eatingDirection = cursor - (Vector2)tongueTransform.position;
	}

	void Eat () {
		RaycastHit2D hit = Physics2D.Raycast (tongueTransform.position, 
		                                      eatingDirection, 
		                                      eatingDistance, nonmunchyLayer);
		if (hit.collider != null && hit.collider.gameObject.GetComponent<Edible> () != null) {
			eatenFood = hit.collider.gameObject;
			eatenFood.GetComponent<Edible> ().SetEaten (true);
			eatenFood.SetActive (false);
		}
	}

	void Spit () {
		eatenFood.SetActive (true);
		eatenFood.transform.position = tongueTransform.position;
		eatenFood.rigidbody2D.velocity = eatingDirection.normalized * spittingForce;
		eatenFood = null;
	}
}
