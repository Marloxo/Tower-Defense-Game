using UnityEngine;
using System.Collections;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public void SetTarget(Node _target)
    {
        this.target = _target;

        ui.SetActive(true);
        transform.position = target.GetBuildPosition(); //which give us node position with offset
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
}
