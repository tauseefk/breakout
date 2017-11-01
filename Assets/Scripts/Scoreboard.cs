using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	private Text _text;


	void Awake() {
		_text = GetComponent<Text> ();
	}

	public void OnScoreUpdate (int score) {
		_text.text = score.ToString();
	}
}
