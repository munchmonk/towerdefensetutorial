using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	private float fireCountdown = 0f;

	[Header("Attributes")]
	public float range = 15f;
	public float fireRate = 1f;
	public float turnSpeed = 10f;

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public GameObject bulletPrefab;
	public Transform firePoint;

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

    	// Shoot
    	if (fireCountdown <= 0f) {
    		Shoot();
    		fireCountdown = 1f / fireRate;
    	}

    	fireCountdown -= Time.deltaTime;
    }

    void Shoot() {
    	// The tutorial said to cast this to GameObject but I don't understand why. Instantiate should return an Object according to the documentation
    	GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    	// The variable type is Bullet because that is the name of our bullet script (Bullet.cs). bullet will contain a reference to that script
    	Bullet bullet = bulletGO.GetComponent<Bullet>();

    	if (bullet != null)
    		bullet.Seek(target);
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
