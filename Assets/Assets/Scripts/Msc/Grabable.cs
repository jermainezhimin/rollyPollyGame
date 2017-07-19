using UnityEngine;
using System.Collections;

public class Grabable : MonoBehaviour {
	public bool isActive { get; private set; }
	public bool isGrabable { get; private set; }

	private void Start () {
		isGrabable = true;
		Release ();
	}

	public void Grabbed () {
		isActive = false;
	}

	public void Release () {
		isActive = true;
	}

	protected void OnTriggerStay2D (Collider2D collider) {
		Lanky lanky = collider.gameObject.GetComponent< Lanky > ();
		if (lanky != null) {
			isGrabable = false;
			if (!isActive) {
				lanky.Release ();
			}
		}
	}

	protected void OnTriggerExit2D (Collider2D collider) {
		Lanky lanky = collider.gameObject.GetComponent< Lanky > ();
		if (lanky != null) {
			isGrabable = true;
		}
	}
}