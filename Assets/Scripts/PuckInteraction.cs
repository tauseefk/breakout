using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuckInteraction : MonoBehaviour {

	private bool _isTethered = true;

	[System.Serializable]
	public class DeathEvent : UnityEvent {}

	[SerializeField]
	private DeathEvent _deathEvent;

	[System.Serializable]
	public class ScoreEvent : UnityEvent<int> {}

	[SerializeField]
	private ScoreEvent _scoreEvent;

	private Rigidbody _rb;

	[SerializeField]
	[Tooltip("Thrust to bounce off the platform")]
	private float _thrust;

	void Start () {
		_rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			_isTethered = false;
		}
	}

	void FixedUpdate()
	{
		if (!_isTethered && Mathf.Approximately (_rb.velocity.y, 0.0f)) {
			gameObject.transform.parent = null;
			_rb.AddForce(- transform.forward * _thrust);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(LayerMask.NameToLayer("Death") == other.gameObject.layer) {
			GameState.decrementLives ();
			_scoreEvent.Invoke (GameState.lives);
			_deathEvent.Invoke ();
			Destroy (gameObject);
		}
	}
}
