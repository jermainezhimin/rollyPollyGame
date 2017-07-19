using UnityEngine;
using System.Collections;

public class Edible : ActiveTrigger {
	bool isEaten = false;
	GameObject ground;

	public void SetEaten (bool eat) { //Something that should only be called by Munchy.
		isEaten = eat;
	}
	public bool GetEaten () {
		return isEaten;
	}

}
