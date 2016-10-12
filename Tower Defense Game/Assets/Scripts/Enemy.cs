using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int moneyGain = 50;

    public GameObject deathEffect;
    private Transform target;
    private int wavepointIndex = 0;
    void Start()
    {
        target = waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        PlayerStats.Money += moneyGain;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Calc the Vector of movement
        Vector3 dir = target.position - transform.position;
        //Translate: to move in this direction 
        //Normalized to make sure that the only thing control our spedd is our speed
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            EndPath();
            //This retrun to make sure the code dont go further in some cases
            return;
        }

        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
