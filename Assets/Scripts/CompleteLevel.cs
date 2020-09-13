using UnityEngine;

public class CompleteLevel : MonoBehaviour {
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    public string nextLevel = "Level02";
	public int levelToUnlock = 2;

    void OnEnable() {
        // Save logic moved here so that a level stays unlocked if we hit "Menu" instead of "Continue" right after winning
        // it was a bug in the very last video
        // :(

        // PlayerPrefs writes to storage an int, float or string to be retrieved next time the application is started
		// "levelReached" is an arbitrary key we chose and it MUST be equal to the one we are using to retrieve this data;
		// it won't throw an error if they differ because PlayerPrefs.GetInt() lets us define a default value to use should it not
		// find any data stored with the given key
		// ref. LevelSelector.cs

		// Change from the tutorial: this if statement ensures we don't overwrite save data (and lock ourselves out of levels)
		// if we complete a level lower than the maximum level we have already reached
		
        if (PlayerPrefs.GetInt("levelReached", 1) < levelToUnlock)
			PlayerPrefs.SetInt("levelReached", levelToUnlock); 
    }

    public void Continue() {
		sceneFader.FadeTo(nextLevel);
    }

    public void Menu() {
		sceneFader.FadeTo(menuSceneName);
	}
}
