using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public List<Enemy> enemies;

    public Wave[] waves;
    private Wave currentWave;
    private int currentWaveIndex;

    private float timeBetweenSpawns;
    private bool stopSpawning = false;
    private bool isSpawningWave = false; //prevent waves from overlapping

    private void Awake()
    {
        currentWave = waves[currentWaveIndex];
        timeBetweenSpawns = currentWave.timeBeforerThisWave;
    }

    private void Update()
    {
        if (stopSpawning || isSpawningWave)
        {
            return;
        }

        StartCoroutine(SpawnWaveWithDelay());
    }


    private IEnumerator SpawnWaveWithDelay()
    {
        isSpawningWave = true;

        yield return StartCoroutine(SpawnWave());
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before starting the next wav
        IncrementWave();

        isSpawningWave = false;
    }

    private IEnumerator SpawnWave()
    {

        for (int i = 0; i < currentWave.numberToSpawn; i++)
        {
            GameObject randEnemy = currentWave.enemiesInWave[Random.Range(0, currentWave.enemiesInWave.Length)];
            int randSpawnPoint = Random.Range(0, transform.childCount);
            Instantiate(randEnemy, transform.GetChild(randSpawnPoint).transform);

            yield return new WaitForSeconds(1f); // Wait for 1 second before spawning the next enemy
        }
    }

    private void IncrementWave()
    {
        if (currentWaveIndex + 1 < waves.Length)
        {
            currentWaveIndex++;
            currentWave = waves[currentWaveIndex];
        } else
        {
            stopSpawning = true;
        }
    }
}