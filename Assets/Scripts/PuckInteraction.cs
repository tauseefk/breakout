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

	// Canvas that fades to game over scene
	[SerializeField]
	private GameObject _fadeToGameOver;

	// Velocity multiplier for collisions with the paddle
	[SerializeField]
	private float _velocityMultiplier;

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

			// negative force launches it upward, because of the orientation
			_rb.AddForce(- transform.forward * _thrust);
		}
	}

	void OnCollisionEnter(Collision other) {
		// Add horizontal velocity to the puck depending on where it collides with the paddle
		if(other.gameObject.layer == LayerMask.NameToLayer("Paddle") && !_isTethered) {
			// Find the vector between the puck and the paddle
			Vector3 _collisionVector = transform.position - other.transform.position;
			_rb.velocity += new Vector3(_collisionVector.normalized.x * _velocityMultiplier, 0f, 0f);
		}
		// Check collision with the bottom boundary and update lives
		if(other.gameObject.layer == LayerMask.NameToLayer("Death")) {
			GameState.decrementLives ();
			_scoreEvent.Invoke (GameState.lives);

			// set fade curtain active, reload scene
			if (GameState.lives > 0) {
				// _fadeToBlack.SetActive (true);
			} else {
				// XXX:TODO create the game over scene
				// _fadeToGameOver.SetActive (true);
			}

			_fadeToBlack.SetActive (true);

			_deathEvent.Invoke ();
			Destroy (gameObject);
		}
	}
}
