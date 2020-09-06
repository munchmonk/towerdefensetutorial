using UnityEngine;

public class Waypoints : MonoBehaviour {
    
    // Static variable so that we can access it from another script without needing to instantiate Waypoints class
    public static Transform[] points;

    void Awake() {
    	points = new Transform[transform.childCount];
    	for(int i = 0; i < points.Length; i++)
    		points[i] = transform.GetChild(i);
    }
}
