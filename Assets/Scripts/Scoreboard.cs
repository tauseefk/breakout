using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	private Text _text;

	[SerializeField]
	private string _statType;


	void Awake() {
		_text = GetComponent<Text> ();
	}

	void Start() {
		switch (_statType) {
		case "Score":
			_text.text = GameState.score.ToString();
			break;
		case "Lives":
			_text.text = GameState.lives.ToString ();
			break;
		}
	}

	/**
	 * @params: takes an integer and updates the UI
	 * 
	 */
	public void OnScoreUpdate (int score) {
		_text.text = score.ToString();
	}
}
