using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour {

	// Event fired when puck collides with the brick
	[System.Serializable]
	public class BrickDestructionEvent : UnityEvent {}

	[SerializeField]
	private BrickDestructionEvent _destructionEvent;

	/**
	 * Fires the event to destroy the brick container, and self destructs
	 * @param: other, which is basically the other object that this object collides with
	 * 
	 */
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Puck")) {
			_destructionEvent.Invoke ();
			Destroy (gameObject);
		}
	}


}
