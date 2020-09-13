using UnityEngine;

// This will be visible and editable in the inspector
[System.Serializable]
// N.B. this class does not inherit from MonoBehaviour since it won't be attached to a Component
public class Wave {
    // The type of enemy to spawn
    public GameObject enemy;

    // How many enemies to spawn
    public int count;

    // The rate at which enemies spawn (rate enemies per second)
    public float rate;
}
