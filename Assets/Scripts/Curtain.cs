using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour {

	// The image that fades in as player dies
	[SerializeField]
	private Image _endCurtain;

	void Start() {
	}

	public void OnGameOver() {
		_endCurtain.enabled = true;
	}
}
