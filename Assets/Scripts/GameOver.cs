using UnityEngine;

// To reference Text
using UnityEngine.UI;

// To reference SceneManager
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	public Text roundsText;

	// Similar to Start(), gets called every time the object is enabled
	void OnEnable() {
		roundsText.text = PlayerStats.Rounds.ToString();
	}

	public void Retry() {
		// Reload the Scene
		// LoadScene needs a buildIndex, which we can get from Build Settings - or in this case, we retrieve automatically to avoid mistakes
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu() {
		Debug.Log("Go to menu");
	}
}

