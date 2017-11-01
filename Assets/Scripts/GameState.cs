using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour
{

	public static GameState instance;

	public static int score { get; private set; }
	public static int lives { get; private set; }


	void Start()
	{
		instance = this;
		score = 0;
		lives = 3;
	}

	public static void incrementScore(int points)
	{
		score += points;
	}

	public static void decrementLives()
	{
		if (lives > 0) {
			Debug.Log ("life XO");
			lives--;
		}
	}
}
