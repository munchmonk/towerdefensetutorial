using UnityEngine;

// Lets us use Event.System (note there is no 's') .IsPointerOverGameObject(). When no parameter is passed, defaults to mouse (as a pointer)
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
	public Color hoverColor;
	public Vector3 positionOffset;

	// We might start the level with a turret prebuilt on a certain node if we wanted to
	[Header("Optional")]
	public GameObject turret;

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

		// Check that a turret is selected
		if (!buildManager.CanBuild)
			return;

		// Check that there isn't already a turret built here
		if (turret != null) {
			Debug.Log("There is already a turret here!");
			return;
		}

		// Build!
		buildManager.BuildTurretOn(this);
	}

	void OnMouseEnter() {
		// Don't highlight a node if there is a UI GameObject over it (i.e. the shop)
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		// Don't highlight the node if we haven't selected a turret from the shop yet
		if (!buildManager.CanBuild)
			return;

		rend.material.color = hoverColor;
	}

	void OnMouseExit() {
		rend.material.color = startColor;
	}
}
