using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject standardTurretPrefab;
    public GameObject MissileLauncherPrefab;
    public GameObject TurretWithPanels;

    private TurretBluePrint turretToBuild;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Basically, without saying too much, you're screwed. Royally and totally.");
            return;
        }
        instance = this;
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
    public bool CanBuild { get { return turretToBuild != null; } }
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
    }

}
