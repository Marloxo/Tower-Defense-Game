using UnityEngine;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    private GameObject turret;
    private Renderer rend;
    private Color startColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    void OnMouseDown()
    {
        //Change the Node Color
        rend.material.color = Color.grey;

        if (turret != null)
        {
            Debug.Log("Can't Build there!!");
            return;
        }

        // Build a turret 
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
         if (turret == null)
            rend.material.color = startColor;      
    }
}
