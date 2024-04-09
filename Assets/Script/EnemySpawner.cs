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

        //if (Time.time >= timeBetweenSpawns)
        //{
        //    StartCoroutine(SpawnWave());
        //    IncrementWave();

        //    timeBetweenSpawns = Time.time + currentWave.timeBeforerThisWave;
        //}

        //foreach (Enemy enemy in enemies)
        //{
        //    if (enemy.isSpawned == false && enemy.spawnTime <= Time.time)
        //    {
        //        if (enemy.randomSpawn)
        //        {
        //            enemy.spawner = Random.Range(0, transform.childCount);
        //        }
        //        // Instantiate the chosen enemy type, at the chosen spawn location
        //        GameObject enemyInstance = Instantiate(enemiesPrefabs[(int)enemy.enemyType], transform.GetChild(enemy.spawner).transform);
        //        enemy.isSpawned = true;
        //    }
        //}
    }


    private IEnumerator SpawnWaveWithDelay()
    {
        isSpawningWave = true;

        yield return StartCoroutine(SpawnWave());

        yield return new WaitForSeconds(2f); // Wait for 2 seconds before starting the next wave

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

            // Wait for 1 second before spawning the next enemy
            yield return new WaitForSeconds(1f); 
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