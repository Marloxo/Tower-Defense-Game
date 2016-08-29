using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    private GameObject turret;
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
        if (buildManager.GetTurretToBuild() == null)
            return;

        //Change the Node Color
        rend.material.color = Color.grey;

        if (turret != null)
        {
            Debug.Log("Can't Build there!! - TODO Display on screen");
            return;
        }

        // Build a turret 
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        if (turret == null)
            rend.material.color = startColor;
    }
}
