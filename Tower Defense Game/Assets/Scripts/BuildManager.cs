using UnityEngine;
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
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
    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

}
