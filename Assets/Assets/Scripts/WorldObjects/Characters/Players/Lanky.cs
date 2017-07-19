using UnityEngine;
using System.Collections;

public class Lanky : Player {
	private Grabable grabbedObject;
	private Vector3 grabbedObjectDifferenceToOrigin;

	protected override void Update () {
		base.Update ();

		if (grabbedObject != null) {
			grabbedObject.transform.position = (Vector3)cursor - grabbedObjectDifferenceToOrigin;
		}
	}

	public override void Jump () {
		if (grabbedObject == null) {
			base.Jump ();
		}
	}

	public override void Move (float dx, float dy) {
		if (grabbedObject == null) {
			base.Move (dx, dy);
		}
	}

	public override void ActionDown () {
		base.ActionDown ();
		Grab ();
	}

	public override void ActionUp () {
		base.ActionUp ();
		Release ();
	}

	public void Grab () {
		Ray ray = new Ray ((Vector3)cursor - Vector3.back, Vector3.back);
		RaycastHit2D hit = Physics2D.GetRayIntersection (ray);

		if (hit.collider != null) {
			Grabable grabbed = hit.collider.GetComponent< Grabable > ();
			if (grabbed != null && grabbed.isGrabable) {
				grabbedObject = grabbed;
				grabbedObjectDifferenceToOrigin = (Vector3)cursor - grabbedObject.transform.position;
				grabbedObject.Grabbed ();
			}
		}
	}

	public void Release () {
		if (grabbedObject != null) {
			grabbedObject.Release ();
		}
		grabbedObject = null;
	}
}
