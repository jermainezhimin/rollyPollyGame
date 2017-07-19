using UnityEngine;
using System.Collections;

public class temp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 temp = new Vector2 (transform.parent.position.x, 3.27f);
		transform.position = temp;
	}
}
