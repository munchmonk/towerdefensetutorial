using UnityEngine;

public class GameManager : MonoBehaviour {
	public static bool GameIsOver;

	public GameObject gameOverUI;
	public GameObject completeLevelUI;

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
		// set to true just in case something else that relies on it needs to check it once we have already completed the level
		GameIsOver = true;

		// Enable the CompleteLevel ui component, which creates the continue button which contains the actual logic to
		// advance to the next level (ref. CompleteLevel.cs)
		completeLevelUI.SetActive(true);
	}
}
