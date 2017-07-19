using UnityEngine;
using System.Collections;

public class CoinRotation : MonoBehaviour {
	public float angularSpeed = 270.0f;

	protected void Update () {
		transform.RotateAround (transform.position, transform.forward, angularSpeed * Time.deltaTime);
	}
}
