using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	public float range = 15f;
	public float turnSpeed = 10f;

	public string enemyTag = "Enemy";

	public Transform partToRotate;

    void Start() {
    	// Calls UpdateTarget() once at the beginning (after 0f seconds) and then once every 0.5 seconds
		InvokeRepeating("UpdateTarget", 0f, 0.5f);		        
    }

    void Update() {
    	if (target == null)
    		return;

    	// Target lock on
    	// Get the rotation we need to apply to face the target
    	Vector3 dir = target.position - transform.position;
    	Quaternion lookRotation = Quaternion.LookRotation(dir);

    	// Lerp smoothes the transition between our current angle (partToRotate.rotation) and our desired angle (lookRotation) at a certain speed
    	// We then convert the quaternion to euler angles because we only want to rotate on the Y axis
    	Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    	partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    void UpdateTarget() {
    	GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
        	float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        	if (distanceToEnemy < shortestDistance) {
        		shortestDistance = distanceToEnemy;
        		nearestEnemy = enemy;
        	}
        }

        if (nearestEnemy != null && shortestDistance <= range)
        	target = nearestEnemy.transform;
        else
        	target = null;
    }

    void OnDrawGizmosSelected() {
    	Gizmos.color = Color.red;
    	Gizmos.DrawWireSphere(transform.position, range);
    }
}
