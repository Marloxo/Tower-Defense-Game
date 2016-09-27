using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    public float countdown = 2f;
    public Text waveCountdownText;
    private int waveNumber = 0;
    void Update()
    {
        if (countdown <= 0f)
        {   //SpawnWave();
            StartCoroutine(SpawnWave());

            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        //To Make Sure dont display negitave number
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);
        
        waveCountdownText.text = String.Format("{0:00.00}",countdown);
    }

    private IEnumerator SpawnWave()
    {
        waveNumber++;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
