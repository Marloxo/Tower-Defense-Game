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
        //Change the Node Color
        rend.material.color = Color.grey;

        if (turret != null) //Node already contain turret
        {
            buildManager.SelectNode(this);
            return;
        }
        if (!buildManager.CanBuild) //No turret selected
            return;

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
