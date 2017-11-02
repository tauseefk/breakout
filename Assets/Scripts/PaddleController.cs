using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Not using physics for the movement of the paddle because it's unnecessary,
 * the movement is restricted to only one axis
 * 
 */
public class PaddleController : MonoBehaviour {

	// Velocity of the paddle
	private Vector3 _velocity;

	// Horizontal speed of the paddle, can be tweaked in the editor
	[SerializeField]
	[Tooltip("Horizontal movement speed of the player.")]
	private float _moveSpeed = 8.0f;

	// XXX:TODO create an animation curve to provide user feedback on paddle contact
	private Animator _playerAnimator;

	// XXX:TODO use a coroutine, to ensure the animation is independent
	private Coroutine _animationCoroutine;

	/**
	 * Update is like life, you just have to create the rules and your object will follow 
	 * 
	 */
	void Update () {
		// Getting axis for user input
		float input = Input.GetAxis(Config.INPUT_AXIS);

		// the horizontal velocity of the paddle
		_velocity.x = input * _moveSpeed;

		// damped by deltaTime to make sure it's cpu speed independent
		transform.position += _velocity * Time.deltaTime;

	}

	// The feedback animation can be triggered here 
	void OnCollisionEnter(Collision other) {
		if (_animationCoroutine != null) {
			StopCoroutine (_animationCoroutine);
			_animationCoroutine = null;
		} else {
			// _animationCoroutine = StartCoroutine (TriggerHitAnimation());
		}
	}


	// XXX:TODO The animation that plays when the puck hits the paddle, for user feedback
	IEnumerator TriggerHitAnimation() {
		_playerAnimator.SetBool ("inContact", true);
		yield return null;
	}
}
