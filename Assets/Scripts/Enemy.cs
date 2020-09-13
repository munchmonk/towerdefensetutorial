using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public float startSpeed = 10f;

	// Hides the variable from the inspector since modifying it manually won't work (as it'll get overwritten by the script), however this keeps it public
	// so that it can be accessed by other scripts (namely enemyMovement)
	[HideInInspector]
	public float speed ;

	public float startHealth = 100;
	private float health;

	public int worth = 50;

	public GameObject deathEffect;

	[Header("Unity Stuff")]
	public Image healthBar;

	// Removes a bug where two lasers locked on the same target grant double money when the enemy dies. Self implemented, might be fixed in a later video
	private bool hasDied = false;

	void Start() {
		speed = startSpeed;
		health = startHealth;
	}

	public void TakeDamage(float amount) {
		health -= amount;
		healthBar.fillAmount = health / startHealth;
		if (health <= 0 && !hasDied) 
			Die();
	}

	public void Slow(float pct) {
		speed = startSpeed * (1f - pct);
	}	

	void Die() {
		hasDied = true;
		
		PlayerStats.Money += worth;

		// Particles
		GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		WaveSpawner.EnemiesAlive--;

		Destroy(gameObject);
	}
}
