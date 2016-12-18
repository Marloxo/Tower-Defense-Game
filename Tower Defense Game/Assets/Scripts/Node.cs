using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;
    BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
    void OnMouseDown()
    {
        //Change the Node Color
        rend.material.color = Color.grey;

        if (turret != null) //Node already contain turret
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild) //No turret selected
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }
    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not enough Money to Build!!");
            return;
        }
        PlayerStats.Money -= bluePrint.cost;
        // Build a turret 
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, this.GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough Money to Upgrade!!");
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);
        // Build new turret 
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, this.GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        //Spawn Cool Effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, this.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBluePrint = null;
    }

    void OnMouseEnter()  //On Mouse Hover
    {
        //check if mouse over UI element then do nothing to avoid overflow clicking.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoeny)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }
    void OnMouseExit()
    {
        if (turret == null)
            rend.material.color = startColor;
    }
}