using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // show class in inspector

public class Enemy
{
    public int spawnTime;
    //public EnemyType enemyType;
    public int spawner; //choose one of the 5 lanes
    public bool randomSpawn; //choose a random lane
    public bool isSpawned;
    //public int scoreValue; // how much score it will add to player
    public EnemyController enemyController; // Store a reference to the EnemyController component
    public GameObject enemyGameObject;
}

public enum EnemyType
{
    None,
    Enemy_Elemental,
    Enemy_Arcane_Fiend,
    Enemy_Shadow_Caster
}
