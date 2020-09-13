using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;

    public void SetTarget(Node _target) {
        target = _target;

        // Node position + offset to center it above the ground instead of below it
        transform.position = target.GetBuildPosition();

        // Update the upgrade button
        if (!target.isUpgraded) {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        // Update the sell button
        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide() {
        ui.SetActive(false);
    }

    public void Upgrade() {
        target.UpgradeTurret();

        // Hide the menu after upgrading
        BuildManager.instance.DeselectNode();
    }

    public void Sell() {
        target.SellTurret();

        // Hide the menu after selling
        BuildManager.instance.DeselectNode();
    }

}
