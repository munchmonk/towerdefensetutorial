using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern
	public static BuildManager instance;

	private GameObject turretToBuild;
	public GameObject standardTurretPrefab;

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one BuildManager in Scene (should be a singleton!)");
			return;
		}
		instance = this;
	}

	void Start() {
		turretToBuild = standardTurretPrefab;
	}

	public GameObject GetTurretToBuild() {
		return turretToBuild;
	}

}
