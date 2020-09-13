using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton pattern
	public static BuildManager instance;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;

	public NodeUI nodeUI;

	public GameObject buildEffect;
	public GameObject sellEffect;

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

	public TurretBlueprint GetTurretToBuild() {
		return turretToBuild;
	}
}