using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using UnityEngine.UI;
using TMPro;


[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public List<Enemy> enemies;

    public Wave[] waves;
    private Wave currentWave;
    private int currentWaveIndex;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;


    private float timeBetweenSpawns;
    private bool stopSpawning = false;
    //private bool isSpawningWave = false; //prevent waves from overlapping
    private float playerScore = 0f;
    private int previousSpawnPoint = -1;


    private void Awake()
    {
        currentWave = waves[currentWaveIndex];
        timeBetweenSpawns = currentWave.timeBeforerThisWave;
    }

    private void Start()
    {

        enemies.Clear();


        // Create object pools for enemies
        foreach (GameObject enemyPrefab in enemiesPrefabs)
        {
            string enemyType = enemyPrefab.GetComponent<EnemyController>().enemyType.ToString();
            ObjectPool.Instance.CreatePool(enemyType, enemyPrefab, 10);
        }


        // Update text UI
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWaveIndex + 1}";
        }

        if (scoreText != null)
        {
            scoreText.text = $"Score: {playerScore}";
        }

    }

    private void Update()
    {
        // Check for defeated enemies and increment player's score
        List<Enemy> defeatedEnemies = enemies.FindAll(enemy => enemy.enemyGameObject == null || !enemy.enemyGameObject.activeSelf);
        foreach (Enemy defeatedEnemy in defeatedEnemies)
        {
            GameObject enemyPrefab = currentWave.enemiesInWave.FirstOrDefault(prefab => prefab != null && prefab.GetComponent<EnemyController>() != null && prefab.GetComponent<EnemyController>().enemyType == defeatedEnemy.enemyController.enemyType);

            if (enemyPrefab != null && defeatedEnemy.enemyController != null)
            {
                int scoreValue = defeatedEnemy.enemyController.scoreValue;
                playerScore += scoreValue;
                if (scoreText != null)
                {
                    scoreText.text = $"Score: {playerScore}";
                }
                enemies.Remove(defeatedEnemy);
            }
        }

    }


    public IEnumerator SpawnWaveWithDelay()
    {
        //isSpawningWave = true;

        while (!stopSpawning)
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(2f); // Wait for 2 seconds before starting the next wave

            // Check if most enemies have been destroyed
            if (enemies.Count <= 1)
            {
                IncrementWave();
            }
            else
            {
                // Wait until most enemies are destroyed
                yield return new WaitUntil(() => enemies.Count <= 1);
                IncrementWave();
            }
        }

        //isSpawningWave = false;
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentWave.numberToSpawn; i++)
        {
            GameObject enemyPrefab = currentWave.enemiesInWave[Random.Range(0, currentWave.enemiesInWave.Length)];
            int randSpawnPoint = Random.Range(0, transform.childCount);

            // Make sure no two consecutive enemies are spawned in the same track
            do
            {
                randSpawnPoint = Random.Range(0, transform.childCount);
            } while (randSpawnPoint == previousSpawnPoint && transform.childCount > 1);

            string enemyType = enemyPrefab.GetComponent<EnemyController>().enemyType.ToString();

            // Get the enemy from the object pool
            GameObject spawnedEnemy = ObjectPool.Instance.GetObjectFromPool(enemyType);

            if (spawnedEnemy != null)
            {
                spawnedEnemy.transform.SetParent(transform.GetChild(randSpawnPoint));
                spawnedEnemy.transform.localPosition = Vector3.zero;
                EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
                enemies.Add(new Enemy
                {
                    enemyGameObject = spawnedEnemy,
                    enemyController = enemyController
                });
            }

            previousSpawnPoint = randSpawnPoint;

            // Wait a bit between enemies
            float randWait = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randWait);
        }

        // Reset the previousSpawnPoint after the wave is spawned
        previousSpawnPoint = -1;
    }

    private void IncrementWave()
    {
        if (currentWaveIndex + 1 < waves.Length)
        {
            currentWaveIndex++;
            currentWave = waves[currentWaveIndex];

            // Clear the enemies list before spawning a new wave
            enemies.Clear();

            // Update the wave text UI
            if (waveText != null)
            {
                waveText.text = $"Wave:  {currentWaveIndex + 1}";
            }

            // update wave difficulty based on player progress
            if (playerScore >= currentWave.scoreThresholdForNextWave)
            {
                float newNumberToSpawn = currentWave.numberToSpawn + (currentWave.numberToSpawn * 0.2f);
                currentWave.SetNumberToSpawn(newNumberToSpawn);

                float newTimeBeforeWave = currentWave.timeBeforerThisWave - 0.2f;
                currentWave.SetTimeBeforerThisWave(newTimeBeforeWave);
            }
            else
            {
                float newNumberToSpawn = currentWave.numberToSpawn - (currentWave.numberToSpawn * 0.1f);
                currentWave.SetNumberToSpawn(newNumberToSpawn);

                float newTimeBeforeWave = currentWave.timeBeforerThisWave + 0.1f;
                currentWave.SetTimeBeforerThisWave(newTimeBeforeWave);
            }
        }
        else
        {
            stopSpawning = true;
        }
    }

    public void HandleEnemyDestruction(EnemyController enemyController)
    {
        // Find the corresponding Enemy instance in the enemies list
        Enemy enemy = enemies.Find(e => e.enemyController == enemyController);

        if (enemy != null)
        {
            // Update the player's score based on the enemy's scoreValue
            playerScore += enemy.enemyController.scoreValue;
            if (scoreText != null)
            {
                scoreText.text = $"Score: {playerScore}";
            }

            // Remove the enemy from the enemies list
            enemies.Remove(enemy);
        }
    }
}