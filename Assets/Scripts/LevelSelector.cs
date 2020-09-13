using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    public SceneFader fader;

    public Button[] levelButtons;

    void Start() {
        // PlayerPrefs is used to store int, float or string values that persist after the application quits
        // "levelReached" is the key we assigned to a certain value we now want to retrieve
        // The second argument (1) is a default value that will be used if "levelreached" can't be retrieved (e.g. on the first time the
        // game is played)
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++) {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    // levelName will be set in the inspector, on the onClick function of the button!
    public void Select(string levelName) {
        fader.FadeTo(levelName);
    }
}
