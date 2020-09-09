using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform target;
	public float speed = 70f;
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
	}

	void HitTarget() {
		// Spawn particles
		GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);

		// Destroy the particles after 2 seconds to not waste memory
		Destroy(effectIns, 2f);

		// Destroy the enemy and the bullet on impact
		Destroy(target.gameObject);
		Destroy(gameObject);
	}
}
