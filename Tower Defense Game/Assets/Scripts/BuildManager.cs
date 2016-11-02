using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject BuildEffect;
    private TurretBluePrint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoeny { get { return PlayerStats.Money >= turretToBuild.cost; } }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Basically, without saying too much, you're screwed. Royally and totally.");
            return;
        }
        instance = this;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough Money to Build!!");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;
        // Build a turret 
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(BuildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }
}
