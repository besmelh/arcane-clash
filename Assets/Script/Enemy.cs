using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // show class in inspector

public class Enemy
{
    public int spawnTime;
    public EnemyType enemyType;
    public int spawner; //choose one of the 5 lanes
    public bool randomSpawner; //choose a random lane
    public bool isSpawned;
}

public enum EnemyType
{
    Enemy_Elemental,
    Enemy_Arcane_Fiend,
    Enemy_Shadow_Caster
}
