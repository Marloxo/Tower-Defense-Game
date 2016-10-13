using UnityEngine;
public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint turretWithPanels;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint LaserBeamer;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectTurretWithPanels()
    {
        buildManager.SelectTurretToBuild(turretWithPanels);
    }
    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(LaserBeamer);
    }
}
