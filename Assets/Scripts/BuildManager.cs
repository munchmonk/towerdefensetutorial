using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern
	public static BuildManager instance;

	private GameObject turretToBuild;

	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one BuildManager in Scene (should be a singleton!)");
			return;
		}
		instance = this;
	}

	public GameObject GetTurretToBuild() {
		return turretToBuild;
	}

	public void SetTurretToBuild(GameObject turret) {
		turretToBuild = turret;
	}

}
