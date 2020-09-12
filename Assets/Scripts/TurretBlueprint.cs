using UnityEngine;

// This means that Unity will save and load the values of this class so that we can access them from the inspector
// This makes it so that another GameObject can declare TurretBlueprint variables and we can initialise them in the inspector
[System.Serializable]
// NOTE: this does not inherit from MonoBehaviour because we don't want to attach it to a GameObject. This script won't be a component, just a standalone script!
public class TurretBlueprint {
	public GameObject prefab;
	public int cost;

	public GameObject upgradedPrefab;
	public int upgradeCost;

}
