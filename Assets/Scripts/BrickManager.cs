using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickManager : MonoBehaviour {

	// Brick destruction particle system
	private ParticleSystem _particleSystem;

	// Number of points awarded when a brick breaks
	[SerializeField]
	private int _brickPoints = 10;

	// Delay before the brick container object is destroyed, so the animation can play out
	[SerializeField]
	private float _particlesPlayTime = 3f;

	// Coroutine returned by the IEnumerator execution, to enable stopping
	private Coroutine _brickDestruction;

	// Event to update the score on UI
	[System.Serializable]
	public class ScoreEvent : UnityEvent<int> {}

	[SerializeField]
	private ScoreEvent _scoreEvent;

	/**
	 * Event fired by the brick gameObject is handeled here,
	 * it updates the score and handles the destruction animation
	 * 
	 */
	public void OnBrickDestruction() {
		if (_brickDestruction == null) {
			GameState.incrementScore (_brickPoints);
			_scoreEvent.Invoke (GameState.score);
			_brickDestruction = StartCoroutine (PlayAnimation());
		}
	}

	// Brick break animation
	IEnumerator PlayAnimation() {
		_particleSystem = gameObject.GetComponentInChildren<ParticleSystem> ();
		_particleSystem.Emit (Random.Range(20, 50));
		yield return new WaitForSeconds(_particlesPlayTime);
		Destroy (gameObject);
		yield return null;
	}
}
