﻿using UnityEngine;
public class Turret : MonoBehaviour
{
    private Transform target;
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    [Header("Unity Setup Fields")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

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

        if (fireCountDown <= 0f)
        {
            shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    private void shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);

    }


    //Built-in Function
    //If you wanted to be always visible call `OnDrawGizmo()` instead of `OnDrawGizmosSelected()`
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
