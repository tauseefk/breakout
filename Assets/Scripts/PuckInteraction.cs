using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuckInteraction : MonoBehaviour {

	// Flag to check if the puck is tethered on the paddle
	private bool _isTethered = true;

	// Event to be fired in-case of collision with the bottom boundary
	[System.Serializable]
	public class DeathEvent : UnityEvent {}

	[SerializeField]
	private DeathEvent _deathEvent;

	// Event to be fired to update the score on UI
	[System.Serializable]
	public class ScoreEvent : UnityEvent<int> {}

	[SerializeField]
	private ScoreEvent _scoreEvent;

	// Rigid body for the puck, to handle addition of forces
	private Rigidbody _rb;

	[SerializeField]
	[Tooltip("Thrust to bounce off the platform")]
	private float _thrust;

	// Canvas that fades to black, for smoother scene transition
	[SerializeField]
	private GameObject _fadeToBlack;

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
		// this is the initial force that should be added to the puck for launching
		if (!_isTethered && Mathf.Approximately (_rb.velocity.y, 0.0f)) {
			gameObject.transform.parent = null;

			// negative force launches it upward
			_rb.AddForce(- transform.forward * _thrust);
		}
	}

	void OnTriggerEnter(Collider other) {
		// Check collision with the bottom boundary and update lives
		if(other.gameObject.layer == LayerMask.NameToLayer("Death")) {
			GameState.decrementLives ();
			_scoreEvent.Invoke (GameState.lives);
			_fadeToBlack.SetActive (true);
			_deathEvent.Invoke ();
			Destroy (gameObject);
		}
	}
}
