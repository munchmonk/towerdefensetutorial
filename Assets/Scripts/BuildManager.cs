﻿using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern
	public static BuildManager instance;

	private TurretBlueprint turretToBuild;

	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one BuildManager in Scene (should be a singleton!)");
			return;
		}
		instance = this;
	}

	// Property; a variable that cannot be set, only read
	public bool CanBuild { get { return turretToBuild != null; } }

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
	}

	public void BuildTurretOn(Node node) {
		if (PlayerStats.Money < turretToBuild.cost) {
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;

		GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		Debug.Log("Turret build, money left: " + PlayerStats.Money);
	}

}
