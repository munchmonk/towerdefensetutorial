using UnityEngine;

public class Enemy : MonoBehaviour {
	public float startSpeed = 10f;

	// Hides the variable from the inspector since modifying it manually won't work (as it'll get overwritten by the script), however this keeps it public
	// so that it can be accessed by other scripts (namely enemyMovement)
	[HideInInspector]
	public float speed ;

	public float health = 100;
	public int worth = 50;

	public GameObject deathEffect;

	// Removes a bug where two lasers locked on the same target grant double money when the enemy dies. Self implemented, might be fixed in a later video
	private bool hasDied = false;

	void Start() {
		speed = startSpeed;
	}

	public void TakeDamage(float amount) {
		health -= amount;
		if (health <= 0) 
			Die();
	}

	public void Slow(float pct) {
		speed = startSpeed * (1f - pct);
	}	

	void Die() {
		if (!hasDied) {
			hasDied = true;
			PlayerStats.Money += worth;
		}

		// Particles
		GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(gameObject);
	}
}
