using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour
{

	public static GameState instance;

	public static int score = 0;
	public static int lives = 3;

	public static int Score {
		get { 
			return score;
		}
		set { 
			score = value;
		}
	}
		
	public static int Lives {
		get { 
			return lives;
		}

		set {
			lives = value;
		}
	}

	void Start()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public static void incrementScore(int points)
	{
		Score += points;
	}

	public static void decrementLives()
	{
		if (Lives > 0) {
			Lives--;
		}
	}
}
