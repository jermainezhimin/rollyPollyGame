using UnityEngine;
using System.Collections;

public class GroundDetector : MonoBehaviour {
	public bool isGrounded {
		get {
			return numGroundEnter != 0;
		}
	}

	private int numGroundEnter = 0;
	private int groundLayer;

	protected void Start () {
		groundLayer = LayerMask.NameToLayer ("Ground");
	}

	protected void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.layer == groundLayer) {
			numGroundEnter ++;
		}
	}

	protected void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.layer == groundLayer) {
			numGroundEnter --;
		}
	}
}
