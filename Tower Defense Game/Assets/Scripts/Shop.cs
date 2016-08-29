using UnityEngine;
public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseMissileLauncher()
    {
        buildManager.SetTurretToBuild(buildManager.MissileLauncherPrefab);
    }
    public void PurchaseTurretWithPanels()
    {
        buildManager.SetTurretToBuild(buildManager.TurretWithPanels);
    }
}
