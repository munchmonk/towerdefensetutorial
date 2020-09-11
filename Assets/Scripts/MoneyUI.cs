using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {
	public Text moneyText;

	void Update() {
		// PlayerStats.Money is a static variable
		moneyText.text = "$" + PlayerStats.Money.ToString();
	}
}
