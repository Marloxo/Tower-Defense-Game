using UnityEngine;
public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    [Header("General")]
    public float range = 15f;
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowAmount = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform PartToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

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
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
            target = null;
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)//if we using laser then turn it off
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        //Target Lock On	
        LockOnTarge();

        if (useLaser)
            Laser();
        else
        {   //fire reqular bullet
            if (fireCountDown <= 0f)
            {
                shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        //Damage Stuff
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        //Graphic Stuff
        if (!lineRenderer.enabled) //Activate Laser
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        //+offeset
        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LockOnTarge()
    {
        //Generate Vector Between A and B
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //To transform lookRotation rotation to Angles (x,y,z)
        //Vector3 rotation = lookRotation.eulerAngles;

        //Enhancement to smooth the move from one state to another
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
