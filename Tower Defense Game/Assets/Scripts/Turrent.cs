using UnityEngine;


public class Turrent : MonoBehaviour
{
    private Transform target;
    public float range = 15f;
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    void Start()
    {//To Invoke a specific method in time seconds, then repeatedly every repeatRate seconds.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;

    }

    void Update()
    {
        if (target == null)
            return;

		//Target Lock On	
        //Generate Vector Between A and B
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //To transform lookRotation rotation to Angles (x,y,z)
        //Vector3 rotation = lookRotation.eulerAngles;

        //Enhancment to smooth the move from one state to another
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }
    //Built-in Function
    //If you wanted to be always visible call `OnDrawGizmo()` instead 
    void OnDrawGizmo()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
