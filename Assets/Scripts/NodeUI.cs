using UnityEngine;

public class NodeUI : MonoBehaviour {
    public GameObject ui;

    private Node target;

    public void SetTarget(Node _target) {
        target = _target;

        // Node position + offset to center it above the ground instead of below it
        transform.position = target.GetBuildPosition();

        ui.SetActive(true);
    }

    public void Hide() {
        ui.SetActive(false);
    }
}
