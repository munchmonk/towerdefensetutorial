using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {
	public Text livesText;

	void Update() {
		// The cast int -> string in this case is redundant as that is done automatically when adding an int and a string together
		livesText.text = PlayerStats.Lives.ToString() + " LIVES";
	}
}
