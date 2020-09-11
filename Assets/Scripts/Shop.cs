using UnityEngine;

public class Shop : MonoBehaviour {
	BuildManager buildManager;

	// TurretBlueprint is the name of the class in the standalone script we created
	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	void Start() {
		buildManager = BuildManager.instance;
	}

	public void SelectStandardTurret() {
		buildManager.SelectTurretToBuild(standardTurret);
	}

	public void SelectMissileLauncher() {
		buildManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer() {
		buildManager.SelectTurretToBuild(laserBeamer);
	}
}
