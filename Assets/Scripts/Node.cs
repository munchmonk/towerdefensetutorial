using UnityEngine;

// Lets us use Event.System (note there is no 's') .IsPointerOverGameObject(). When no parameter is passed, defaults to mouse (as a pointer)
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	public Color hoverColor;
	public Vector3 positionOffset;

	private Renderer rend;
	private Color startColor;

	private GameObject turret;

	BuildManager buildManager;

	void Start() {
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
	}

	void OnMouseDown() {
		// Don't build a turret if the mouse is over a UI GameObject (i.e. the shop menu) so that we don't build by accident
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (buildManager.GetTurretToBuild() == null)
			return;

		if (turret != null) {
			Debug.Log("There is already a turret here!");
			return;
		}

		GameObject turretToBuild = buildManager.GetTurretToBuild();
		turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
	}

	void OnMouseEnter() {
		// Don't highlight a node if there is a UI GameObject over it (i.e. the shop)
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		// Don't highlight the node if we haven't selected a turret from the shop yet
		if (buildManager.GetTurretToBuild() == null)
			return;

		rend.material.color = hoverColor;
	}

	void OnMouseExit() {
		rend.material.color = startColor;
	}
}
