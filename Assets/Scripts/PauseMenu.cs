﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject ui;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
            Toggle();
    }

    public void Toggle() {
        // activeSelf is true if a GameObject is enabled and false otherwise
        ui.SetActive(!ui.activeSelf);    

        if (ui.activeSelf)
            // Time.timeScale changes how "fast" the time goes: normal time is 1f; not moving at all is 0f; double speed is 2f, etc.
            // N.B. If changing it to anything other than 0f we need to manually set Time.fixedDeltaTime as well not to mess it up
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Retry() {
        Toggle();

        // SceneFader implemented the load with a name (string), but it's normally a buildIndex (int) 
        // (check LoadScene documentation for reference)
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
