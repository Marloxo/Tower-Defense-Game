using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellAmount;
    private Node target;
    public void SetTarget(Node _target)
    {
        this.target = _target;

        ui.SetActive(true);
        transform.position = target.GetBuildPosition(); //which give us node position with offset

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Maximized";
            upgradeButton.interactable = false;
        }
        sellAmount.text = "$" + target.turretBluePrint.GetSellAmount();
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
