using UnityEngine;

public class GameManager : MonoBehaviour {
	public static bool GameIsOver;

	public GameObject gameOverUI;

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
}
