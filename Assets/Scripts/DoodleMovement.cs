using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoodleMovement : MonoBehaviour {

	private const string INPUT_AXIS = "Horizontal";

	[SerializeField]
	[Tooltip("Horizontal movement speed of the player.")]
	private float _moveSpeed = 8.0f;

	[SerializeField]
	private float _gravity = -30.0f;

	[SerializeField]
	[Tooltip("Y speed when bouncing off a platform (m/s)")]
	private float _bounceSpeed = 15.0f;

	private Vector3 _velocity;

	private int _maxHeight = 0;

	private int _currentHeight = 0;

	private bool flag;

	[System.Serializable]
	public class MaxHeightEvent : UnityEvent<float> {}
	[SerializeField]
	private MaxHeightEvent _maxHeightEvent;

	private Collider _playerCollider;

	private Animator _playerAnimator;

	private Coroutine _animationCoroutine;

	private ParticleSystem _playerParticleSystem;
	private AudioSource _playerAudioSource;

	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
		_currentHeight = 0;
		_playerCollider = GetComponent<Collider> ();
		_playerAnimator = GetComponent<Animator> ();
		_playerParticleSystem = GetComponentInChildren<ParticleSystem> ();
		_playerAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		_currentHeight = Mathf.FloorToInt(transform.position.y);
		float input = Input.GetAxis(INPUT_AXIS);

		_velocity.x = input * _moveSpeed;
		_velocity.y += _gravity * Time.deltaTime;

		transform.position += _velocity * Time.deltaTime;
		if (_currentHeight > _maxHeight) {
			_maxHeight = _currentHeight;
			_maxHeightEvent.Invoke (_maxHeight);
		} else if(_currentHeight < _maxHeight - Config.FALL_THRESHOLD) {
			_playerCollider.enabled = false;
			StartCoroutine (LoadGameOverScene());
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Platform")) {
			_velocity.y = _bounceSpeed;
			if (_playerParticleSystem.isPlaying && flag == false) {
				_playerParticleSystem.Stop ();
				flag = true;
			} else {
				_playerParticleSystem.Play ();
				_playerAudioSource.Play ();
			}
			if (_animationCoroutine == null) {
				_animationCoroutine = StartCoroutine (TriggerSquishAnimation ());
			} else {
				StopCoroutine (TriggerSquishAnimation());
				_animationCoroutine = null;
				_animationCoroutine = StartCoroutine (TriggerSquishAnimation ());
			}
		}
	}

	IEnumerator TriggerSquishAnimation() {
		_playerAnimator.SetBool ("inAir", false);
		yield return new WaitForSeconds (1f);
		_playerAnimator.SetBool ("inAir", true);
		yield return null;
	}

	IEnumerator LoadGameOverScene () {
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene ("GameOver");
		yield return null;
	}
}
