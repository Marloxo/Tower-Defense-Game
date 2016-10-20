using System;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int moneyGain = 50;
    public GameObject deathEffect;
    void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
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
    /// <summary>
    /// Slow is called from turret script,
    /// slowAmount between [0-1] so it's percentage
    ///We multiply by startSpeed to allow the slow function to decrease our speed
    /// only one time from the default speed 
    /// </summary>
    public void Slow(float slowAmount)
    {
        speed = startSpeed * (1f - slowAmount);
    }
}
