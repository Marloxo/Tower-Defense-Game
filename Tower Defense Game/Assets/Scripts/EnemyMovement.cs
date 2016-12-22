using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy _enemy;
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        target = waypoints.points[0];
    }
    void Update()
    {
        //Calc the Vector of movement
        Vector3 dir = target.position - transform.position;
        //Translate: to move in this direction 
        //Normalized to make sure that the only thing control our speed is our speed
        transform.Translate(dir.normalized * _enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            GetNextWayPoint();

        //reset Enemy speed if turret not shooting us
        _enemy.speed = _enemy.startSpeed;
    }
    private void GetNextWayPoint()
    {
        if (wavepointIndex >= waypoints.points.Length - 1)
        {
            EndPath();
            //This return to make sure the code don't go further in some cases
            return;
        }

        wavepointIndex++;
        target = waypoints.points[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}