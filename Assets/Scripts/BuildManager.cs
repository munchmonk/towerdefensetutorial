using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern
	public static BuildManager instance;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;

	public NodeUI nodeUI;

	public GameObject buildEffect;

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one BuildManager in Scene (should be a singleton!)");
			return;
		}
		instance = this;
	}

	// Property; a variable that cannot be set, only read
	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

	public void SelectNode(Node node) {
		// If we click the node that is already selected, hide the UI
		if (selectedNode == node) {
			DeselectNode();
			return;
		}
			
		selectedNode = node;
		turretToBuild = null;
		nodeUI.SetTarget(node);
	}

	public void DeselectNode() {
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(TurretBlueprint turret) {
		turretToBuild = turret;
		DeselectNode();
	}

	public void BuildTurretOn(Node node) {
		if (PlayerStats.Money < turretToBuild.cost) {
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= turretToBuild.cost;

		GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		// Create particles and delete them after 5 seconds
		GameObject effect = (GameObject) Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
	}

}