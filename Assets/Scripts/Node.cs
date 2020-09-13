using UnityEngine;

// Lets us use Event.System (note there is no 's') .IsPointerOverGameObject(). When no parameter is passed, defaults to mouse (as a pointer)
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;

	// We need these variables to be public but we don't want them to be editable in the inspector
	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	public Vector3 GetBuildPosition() {
		return transform.position + positionOffset;
	}

	void Start() {
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
	}

	void OnMouseDown() {
		// Don't build a turret if the mouse is over a UI GameObject (i.e. the shop menu) so that we don't build by accident
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		// If there is a turret built here, select the node
		if (turret != null) {
			buildManager.SelectNode(this);
		}

		// Check that a turret is selected
		if (!buildManager.CanBuild)
			return;

		// Build!
		BuildTurret(buildManager.GetTurretToBuild());
	}

	void BuildTurret(TurretBlueprint blueprint) {
		if (PlayerStats.Money < blueprint.cost) {
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= blueprint.cost;

		GameObject _turret = (GameObject) Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;
		turretBlueprint = blueprint;

		// Create particles and delete them after 5 seconds
		GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
	}

	public void UpgradeTurret() {
		if (PlayerStats.Money < turretBlueprint.upgradeCost) {
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

		// Remove the old turret to avoid having two on the same node
		Destroy(turret);

		GameObject _turret = (GameObject) Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		// Create particles and delete them after 5 seconds
		GameObject effect = (GameObject) Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;
	}

	public void SellTurret() {
		PlayerStats.Money += turretBlueprint.GetSellAmount();

		// Create particles and delete them after 5 seconds
		GameObject effect = (GameObject) Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(turret);
		turretBlueprint = null;
		isUpgraded = false;

	}

	void OnMouseEnter() {
		// Don't highlight a node if there is a UI GameObject over it (i.e. the shop)
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		// Don't highlight the node if we haven't selected a turret from the shop yet
		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney) 
			// Highlight the node we mouse over
			rend.material.color = hoverColor;
		else
			// Highlight with a different colour if we can't afford it
			rend.material.color = notEnoughMoneyColor;
	}

	void OnMouseExit() {
		rend.material.color = startColor;
	}
}
