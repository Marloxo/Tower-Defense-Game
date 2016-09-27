using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    [HeaderAttribute("Optional")]
    public GameObject turret;
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
        if (!buildManager.CanBuild)
            return;

        //Change the Node Color
        rend.material.color = Color.grey;

        if (turret != null)
        {
            Debug.Log("Can't Build there!! - TODO Display on screen");
            return;
        }
        buildManager.BuildTurretOn(this);
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    void OnMouseEnter()
    {
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
