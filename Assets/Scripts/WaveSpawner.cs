using UnityEngine;

// Lets us use IEnumerator and Coroutines
using System.Collections;

// Lets us reference a Text
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    public static int EnemiesAlive = 0;

    // Array of Wave, a class defined in Wave.cs
    public Wave[] waves;

    public Transform spawnPoint;
    public Text waveCountdownText;
    public float timeBetweenWaves = 5f;

    public GameManager gameManager;

    private float countdown = 2f;
    private int waveIndex = 0;

    void Start() {
        // Removes a bug where restarting the level keeps EnemiesAlive not at 0 (due to it being static)
        EnemiesAlive = 0;
    }

    void Update() {
        // Only countdown if we have killed the wave
        if (EnemiesAlive > 0)
            return;

        // Win the level - change from the tutorial, it was bugged
        if (waveIndex == waves.Length) {
            gameManager.WinLevel();

            // Disable this script to stop spawning waves after we cleared them all
            this.enabled = false;
        }

    	if (countdown <= 0f) {
    		StartCoroutine(SpawnWave());
    		countdown = timeBetweenWaves;

            // Return so that the countdown remains zero whilst a wave is alive
            return;
    	}

    	countdown -= Time.deltaTime;

        // Make sure it's not accidentally negative
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // Format it to show two deciml places
    	waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave() {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        // Set the total number of EnemiesAlive here so that it doesn't bug out if we kill all the visible ones before the whole
        // wave is done spawning
        EnemiesAlive = wave.count;

    	for(int i = 0; i < wave.count; i++) {
    		SpawnEnemy(wave.enemy);
    		yield return new WaitForSeconds(1f / wave.rate);
    	}

        waveIndex++;

        /*
        if (waveIndex == waves.Length) {
            gameManager.WinLevel();

            // Disable this script to stop spawning waves after we cleared them all
            this.enabled = false;
        }
        */
    }

    void SpawnEnemy(GameObject enemy) {
    	Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
