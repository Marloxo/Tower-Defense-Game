using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public Wave[] waves;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    public float countdown = 2f;
    public Text waveCountdownText;
    private int waveNumber = 0;
    void Update()
    {
        if (enemiesAlive > 0) //do nothing until no enemies Alive
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());

            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        //To Make Sure don't display negative number
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = String.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
        if (waveNumber == waves.Length) //player won all waves!! then level won
        {
            this.enabled = false;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }
}
