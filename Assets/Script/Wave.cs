using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves", order = 1)]
public class Wave : ScriptableObject
{
    [field: SerializeField] public GameObject[] enemiesInWave { get; private set; }
    [field: SerializeField] public float timeBeforerThisWave { get; private set; }
    [field: SerializeField] public float numberToSpawn { get; private set; }
    [field: SerializeField] public float scoreThresholdForNextWave { get; private set; }

    public void SetNumberToSpawn(float value) { numberToSpawn = value; }
    public void SetTimeBeforerThisWave(float value) { timeBeforerThisWave = value; }
}