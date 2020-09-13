using UnityEngine;

public class LevelSelector : MonoBehaviour {
    public SceneFader fader;

    // levelName will be set in the inspector, on the onClick function of the button!
    public void Select(string levelName) {
        fader.FadeTo(levelName);
    }
}
