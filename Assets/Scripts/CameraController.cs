using UnityEngine;

public class CameraController : MonoBehaviour {
	private bool doMovement = true;

	public float panSpeed = 30f;
	public float panBorderthickness = 10f;
	public float scrollSpeed = 5f;

	public float minY = 10f;
	public float maxY = 80f;

	void Update() {
		if (GameManager.GameIsOver) {
			// Disable this script. "this." is redundant, "enabled = false" works just as well
			this.enabled = false;
			return;
		}

		// ------------------------------------------------------------------------------------------------------------
		// Escape disables (toggles) camera movement - useful for working in the inspector
		if (Input.GetKeyDown(KeyCode.Escape))
			doMovement = !doMovement;

		if (!doMovement)
			return;

		// ------------------------------------------------------------------------------------------------------------
		// Moving in the 4 directions - WASD and mouse near screen edges
		// We Translate relative to world coordinates, not local (to the camera) so it doesn't tilt awkwardly

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderthickness) 
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("s") || Input.mousePosition.y <= panBorderthickness) 
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderthickness) 
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderthickness) 
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

		// ------------------------------------------------------------------------------------------------------------
		// Zooming in/out - mouse scrollwheel

		// This name comes from Edit -> Project Settings -> Input Manager and it's the name Unity gave to the scrollwheel
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		Vector3 pos = transform.position;
		// We multiply by 100 because the scrollwheel values are very low
		pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
		// Clamp restricts the first argument between a min and a max value
		pos.y = Mathf.Clamp(pos.y, minY, maxY);
		transform.position = pos;
	}
}
