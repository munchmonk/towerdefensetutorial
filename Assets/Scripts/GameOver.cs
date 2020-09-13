using UnityEngine;

// To reference SceneManager
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public string menuSceneName = "MainMenu";
	public SceneFader sceneFader;

	public void Retry() {
		// Reload the Scene

		// LoadScene needs a buildIndex, which we can get from Build Settings - or in this case, we retrieve automatically to avoid mistakes
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		// We now use a SceneFader but I left the above for future reference
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu() {
		sceneFader.FadeTo(menuSceneName);
	}
}

