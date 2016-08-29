using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    public GameObject standardTurretPrefab;
    public GameObject MissileLauncherPrefab;
    public GameObject TurretWithPanels;

    private GameObject turretToBuild;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Basically, without saying too much, you're screwed. Royally and totally.");
            return;
        }
        instance = this;
    }
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
