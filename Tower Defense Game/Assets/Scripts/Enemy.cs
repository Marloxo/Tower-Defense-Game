using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    private float startHealth = 100;
    public float health = 100;
    public int moneyGain = 50;
    public GameObject deathEffect;
    private bool isDead = false;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
            Die();
    }
    private void Die()
    {
        isDead = true;

        PlayerStats.Money += moneyGain;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.enemiesAlive--;

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