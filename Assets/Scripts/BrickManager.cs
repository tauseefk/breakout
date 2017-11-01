using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickManager : MonoBehaviour {

	private ParticleSystem _particleSystem;

	[SerializeField]
	private int _brickPoints = 100;

	[SerializeField]
	private float _particlesPlayTime = 3f;

	private Coroutine _brickDestruction;

	[System.Serializable]
	public class ScoreEvent : UnityEvent<int> {}

	[SerializeField]
	private ScoreEvent _scoreEvent;

	public void OnBrickDestruction() {
		if (_brickDestruction == null) {
			GameState.incrementScore (_brickPoints);
			_scoreEvent.Invoke (GameState.score);
			_brickDestruction = StartCoroutine (PlayAnimation());
		}
	}

	IEnumerator PlayAnimation() {
		_particleSystem = gameObject.GetComponentInChildren<ParticleSystem> ();
		_particleSystem.Emit (Random.Range(20, 50));
		yield return new WaitForSeconds(_particlesPlayTime);
		Destroy (gameObject);
		yield return null;
	}
}
