using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour {

	private ParticleSystem _particleSystem;

	[System.Serializable]
	public class BrickDestructionEvent : UnityEvent {}

	[SerializeField]
	private BrickDestructionEvent _destructionEvent;
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Puck")) {
			_destructionEvent.Invoke ();
			Destroy (gameObject);
		}
	}


}
