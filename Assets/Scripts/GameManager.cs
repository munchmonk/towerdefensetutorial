using UnityEngine;

public class GameManager : MonoBehaviour {
	public static bool GameIsOver;

	public GameObject gameOverUI;

	public string nextLevel = "Level02";
	public int levelToUnlock = 2;

	public SceneFader sceneFader;

	void Start() {
		// We set it here because it's static and we don't want it to stay true if we restart the game. Start() is called everytime we load a scene
		GameIsOver = false;
	}

	void Update() {
		if (GameIsOver)
			return;
			
		if (PlayerStats.Lives <= 0) {
			EndGame();
		}
	}

	void EndGame() {
		GameIsOver = true;

		// SetActive -> GameObject, enabled -> single Component
		gameOverUI.SetActive(true);
	}

	public void WinLevel() {
		Debug.Log("Level won!");

		// PlayerPrefs writes to storage an int, float or string to be retrieved next time the application is started
		// "levelReached" is an arbitrary key we chose and it MUST be equal to the one we are using to retrieve this data;
		// it won't throw an error if they differ because PlayerPrefs.GetInt() lets us define a default value to use should it not
		// find any data stored with the given key
		// ref. LevelSelector.cs

		// Change from the tutorial: this if statement ensures we don't overwrite save data (and lock ourselves out of levels)
		// if we complete a level lower than the maximum level we have already reached
		if (PlayerPrefs.GetInt("levelReached", 1) < levelToUnlock)
			PlayerPrefs.SetInt("levelReached", levelToUnlock); 
		sceneFader.FadeTo(nextLevel);
	}
}
