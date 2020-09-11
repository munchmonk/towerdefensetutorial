using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform target;

	public float speed = 70f;
	public float explosionRadius = 0f;
	public int damage = 50;
	public GameObject impactEffect;

	// To be called by the turret that instantiates this bullet to pass it its target
	public void Seek(Transform _target) {
		target = _target;
	}

	void Update() {
		// Check if the target has already died and kill the bullet
		if (target == null) {
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		// We have hit the target
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget();
			return;
		}

		// No hit yet - move the bullet, relative to the world space (not the local one)
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

		// Rotate the bullet to always face the target
		transform.LookAt(target);
	}

	void HitTarget() {
		// Spawn particles
		GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);

		// Destroy the particles after 5 seconds to not waste memory
		Destroy(effectIns, 5f);

		// AoE
		if (explosionRadius > 0f) {
			Explode();
		}

		// Single target
		else {
			Damage(target);
		}

		// Destroy the bullet on impact
		Destroy(gameObject);
	}

	void Damage(Transform enemy) {
		// Enemy is the name of the class inside the Enemy script
		Enemy e = enemy.GetComponent<Enemy>();

		// In case the enemy doesn't have a script for whatever reason
		if (e != null)
			e.TakeDamage(damage);
	}

	void Explode() {
		// Creates a sphere at the given position and returns every gameOobject it overlaps with
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider collider in colliders) 
			if (collider.tag == "Enemy")
				Damage(collider.transform);
	}

	// When selecting the bullet draws a wire sphere (just the outline) around the bullet of radius explosionRadius, not visible during the game
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}













