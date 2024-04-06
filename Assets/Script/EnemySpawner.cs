using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPrefabs;
    public List<Enemy> enemies;

    private void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.isSpawned == false && enemy.spawnTime <= Time.time)
            {
                // Instantiate the chosen enemy type, at the chosen spawn location
                GameObject enemyInstance = Instantiate(enemiesPrefabs[(int)enemy.enemyType], transform.GetChild(enemy.spawner).transform);
                enemy.isSpawned = true;
            }
        }
    }
}