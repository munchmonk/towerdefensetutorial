using UnityEngine;

// Makes it that so this script won't work without a Component called Enemy
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	void Start() {
		// Finds a Component of type Enemy on the same GameObject this script sits on
		enemy = GetComponent<Enemy>();

		// We can do this because class Waypoints has a static member points
		target = Waypoints.points[0];
	}    

	void Update() {
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f) 
			GetNextWaypoint();

		// Resets the speed so that if we aren't hit by  laser anymore we don't stay slowed down
		enemy.speed = enemy.startSpeed;
	}

	void GetNextWaypoint() {
		if (wavepointIndex >= Waypoints.points.Length - 1) {
			EndPath();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void EndPath() {	
		PlayerStats.Lives--;
		// gameObject refers to self
		Destroy(gameObject);
	}
}
