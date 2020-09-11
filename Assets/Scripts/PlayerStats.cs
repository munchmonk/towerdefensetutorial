using UnityEngine;

public class PlayerStats : MonoBehaviour {

	// Static because we want to be able to access it without having to instantiate PlayerStats
	// Note: static variables carry over from a scene to another (including when reloading the same scene e.g. when resetting the level)
	public static int Money;
	public int startMoney = 400;

	void Start() {
		Money = startMoney;
	}
}
